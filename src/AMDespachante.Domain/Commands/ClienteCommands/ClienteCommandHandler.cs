using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Events.ClienteEvents;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace AMDespachante.Domain.Commands.ClienteCommands
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<NovoClienteCommand, ValidationResult>,
        IRequestHandler<AtualizarClienteCommand, ValidationResult>,
        IRequestHandler<RemoverClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(NovoClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            _validationResult.Errors.Clear();

            var cliente = new Cliente(message.Nome, 
                message.Cpf,
                message.Telefone, 
                message.Email, 
                message.EhEstacionamento, 
                message.PagaMensalidade, 
                message.ValorMensalidade, 
                message.DataProximoVencimento,
                message.Veiculos.Select(v => 
                    new Veiculo(v.Placa, v.Renavam, v.Modelo, v.AnoFabricacao, v.AnoModelo)).ToList());

            _clienteRepository.Add(cliente);

            cliente.AddEvent(new ClienteCriadoEvent(cliente));

            return await Commit(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            _validationResult.Errors.Clear();

            var cliente = await _clienteRepository.GetById(message.Id);

            if(cliente is null)
            {
                AddError("Cliente não encontrado");
                return _validationResult;
            }

            cliente.Nome = message.Nome;
            cliente.Cpf = message.Cpf;
            cliente.Telefone = message.Telefone;
            cliente.Email = message.Email;
            cliente.EhEstacionamento = message.EhEstacionamento;
            cliente.PagaMensalidade = message.PagaMensalidade;
            cliente.ValorMensalidade = message.ValorMensalidade;
            cliente.DataProximoVencimento = message.DataProximoVencimento;

            cliente.Veiculos = message.Veiculos.Select(v =>
            new Veiculo
            {
                Id = Guid.Empty,
                Placa = v.Placa,
                Renavam = v.Renavam,
                Modelo = v.Modelo,
                AnoFabricacao = v.AnoFabricacao,
                AnoModelo = v.AnoModelo
            }).ToList();

            _clienteRepository.Update(cliente);

            cliente.AddEvent(new ClienteAtualizadoEvent(cliente));

            return await Commit(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoverClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            _validationResult.Errors.Clear();

            var cliente = await _clienteRepository.GetById(message.Id);

            if (cliente is null)
            {
                AddError("Cliente não encontrado");
                return _validationResult;
            }

            _clienteRepository.Delete(cliente);

            cliente.AddEvent(new ClienteRemovidoEvent(message.Id));

            return await Commit(_clienteRepository.UnitOfWork);
        }
    }
}
