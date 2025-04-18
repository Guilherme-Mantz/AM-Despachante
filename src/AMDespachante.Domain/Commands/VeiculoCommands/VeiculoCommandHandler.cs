using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Events.VeiculoEvents;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace AMDespachante.Domain.Commands.VeiculoCommands
{
    public class VeiculoCommandHandler(IVeiculoRepository veiculoRepository) : CommandHandler,
        IRequestHandler<NovoVeiculoCommand, ValidationResult>,
        IRequestHandler<AtualizarVeiculoCommand, ValidationResult>,
        IRequestHandler<GerenciarVeiculosClienteCommand, ValidationResult>,
        IRequestHandler<RemoverVeiculoCommand, ValidationResult>
    {
        private readonly IVeiculoRepository _veiculoRepository = veiculoRepository;

        public async Task<ValidationResult> Handle(NovoVeiculoCommand message, CancellationToken cancellationToken)
        {
            try
            {
                _validationResult.Errors.Clear();

                if (!message.IsValid()) return message.ValidationResult;

                var veiculo = new Veiculo(message.Placa,
                    message.Renavam,
                    message.TipoVeiculo,
                    message.Modelo,
                    message.AnoFabricacao,
                    message.AnoModelo,
                    (Guid)message.ClienteId);

                if (await _veiculoRepository.PlacaExists(veiculo.Id, veiculo.Placa))
                {
                    AddError($"Já existe um veículo cadastrado com a placa {message.Placa}.");
                    return _validationResult;
                }

                _veiculoRepository.Add(veiculo);

                veiculo.AddEvent(new VeiculoCriadoEvent(veiculo));

                return await Commit(_veiculoRepository.UnitOfWork);
            }
            finally
            {
                _veiculoRepository.UnitOfWork.Reset();
            }
        }

        public async Task<ValidationResult> Handle(AtualizarVeiculoCommand message, CancellationToken cancellationToken)
        {
            try
            {
                _validationResult.Errors.Clear();

                if (!message.IsValid()) return message.ValidationResult;

                var veiculo = await _veiculoRepository.GetById(message.Id);

                if (veiculo is null)
                {
                    AddError("Veículo não encontrado");
                    return _validationResult;
                }

                if (veiculo.Placa != message.Placa && 
                    await _veiculoRepository.PlacaExists(veiculo.Id, message.Placa))
                {
                    AddError($"Já existe um veículo cadastrado com a placa {message.Placa}.");
                    return _validationResult;
                }

                veiculo.Placa = message.Placa;
                veiculo.Renavam = message.Renavam;
                veiculo.TipoVeiculo = message.TipoVeiculo;
                veiculo.Modelo = message.Modelo;
                veiculo.AnoFabricacao = message.AnoFabricacao;
                veiculo.AnoModelo = message.AnoModelo;
                veiculo.ClienteId = message.ClienteId;

                _veiculoRepository.Update(veiculo);

                veiculo.AddEvent(new VeiculoAtualizadoEvent(veiculo));

                return await Commit(_veiculoRepository.UnitOfWork);
            }
            finally
            {
                _veiculoRepository.UnitOfWork.Reset();
            }
        }

        public async Task<ValidationResult> Handle(GerenciarVeiculosClienteCommand message, CancellationToken cancellationToken)
        {
            try
            {
                var veiculosExistentes = await _veiculoRepository.GetByClienteIdAsync(message.ClienteId);
                var veiculosDict = veiculosExistentes.ToDictionary(v => v.Placa.ToUpper());
                var placasAtuais = new HashSet<string>(message.Veiculos.Select(v => v.Placa.ToUpper()));

                foreach (var veiculo in message.Veiculos)
                {
                    var placaUpper = veiculo.Placa.ToUpper();

                    if (veiculosDict.TryGetValue(placaUpper, out var veiculoExistente))
                    {
                        AtualizarVeiculo(veiculoExistente, veiculo);
                    }
                    else
                    {
                        AdicionarNovoVeiculo(veiculo, message.ClienteId);
                    }
                }

                RemoverVeiculosNaoPresentes(veiculosExistentes, placasAtuais);

                return await Commit(_veiculoRepository.UnitOfWork);
            }
            finally
            {
                _veiculoRepository.UnitOfWork.Reset();
            }
        }

        public async Task<ValidationResult> Handle(RemoverVeiculoCommand message, CancellationToken cancellationToken)
        {
            _validationResult.Errors.Clear();

            if (!message.IsValid()) return message.ValidationResult;

            var veiculo = await _veiculoRepository.GetById(message.Id);

            if (veiculo is null)
            {
                AddError("Veículo não encontrado");
                return _validationResult;
            }

            _veiculoRepository.Delete(veiculo);

            veiculo.AddEvent(new VeiculoRemovidoEvent(veiculo.Id));

            return await Commit(_veiculoRepository.UnitOfWork);
        }

        private void AtualizarVeiculo(Veiculo veiculoExistente, AtualizarVeiculoCommand veiculoNovo)
        {
            veiculoExistente.Renavam = veiculoNovo.Renavam;
            veiculoExistente.TipoVeiculo = veiculoNovo.TipoVeiculo;
            veiculoExistente.Modelo = veiculoNovo.Modelo;
            veiculoExistente.AnoFabricacao = veiculoNovo.AnoFabricacao;
            veiculoExistente.AnoModelo = veiculoNovo.AnoModelo;
            _veiculoRepository.Update(veiculoExistente);
        }

        private void AdicionarNovoVeiculo(AtualizarVeiculoCommand veiculo, Guid clienteId)
        {
            var novoVeiculo = new Veiculo
            {
                Placa = veiculo.Placa,
                Renavam = veiculo.Renavam,
                TipoVeiculo = veiculo.TipoVeiculo,
                Modelo = veiculo.Modelo,
                AnoFabricacao = veiculo.AnoFabricacao,
                AnoModelo = veiculo.AnoModelo,
                ClienteId = clienteId
            };

            _veiculoRepository.Add(novoVeiculo);
        }

        private void RemoverVeiculosNaoPresentes(IEnumerable<Veiculo> veiculosExistentes, HashSet<string> placasAtuais)
        {
            foreach (var veiculo in veiculosExistentes.Where(v => !placasAtuais.Contains(v.Placa.ToUpper())))
            {
                _veiculoRepository.Delete(veiculo);
            }
        }
    }
}
