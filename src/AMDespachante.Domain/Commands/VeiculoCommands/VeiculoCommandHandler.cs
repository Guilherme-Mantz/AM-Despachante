using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Events.VeiculoEvents;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace AMDespachante.Domain.Commands.VeiculoCommands
{
    public class VeiculoCommandHandler : CommandHandler,
        IRequestHandler<NovoVeiculoCommand, ValidationResult>,
        IRequestHandler<AtualizarVeiculoCommand, ValidationResult>,
        IRequestHandler<RemoverVeiculoCommand, ValidationResult>
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoCommandHandler(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public async Task<ValidationResult> Handle(NovoVeiculoCommand message, CancellationToken cancellationToken)
        {
            _validationResult.Errors.Clear();

            if (!message.IsValid()) return message.ValidationResult;

            var veiculo = new Veiculo(message.Placa, 
                message.Renavam,
                message.Modelo, 
                message.AnoFabricacao,
                message.AnoModelo, 
                (Guid)message.ClienteId);

            _veiculoRepository.Add(veiculo);

            veiculo.AddEvent(new VeiculoCriadoEvent(veiculo));

            return await Commit(_veiculoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarVeiculoCommand message, CancellationToken cancellationToken)
        {
            _validationResult.Errors.Clear();

            if (!message.IsValid()) return message.ValidationResult;

            var veiculo = await _veiculoRepository.GetById(message.Id);

            if(veiculo is null)
            {
                AddError("Veículo não encontrado");
                return _validationResult;
            }

            veiculo.Placa = message.Placa;
            veiculo.Renavam = message.Renavam;
            veiculo.Modelo = message.Modelo;
            veiculo.AnoFabricacao = message.AnoFabricacao;
            veiculo.AnoModelo = message.AnoModelo;
            veiculo.ClienteId = (Guid)message.ClienteId;

            _veiculoRepository.Update(veiculo);

            veiculo.AddEvent(new VeiculoAtualizadoEvent(veiculo));

            return await Commit(_veiculoRepository.UnitOfWork);
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
    }
}
