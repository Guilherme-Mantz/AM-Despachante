using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Events.AtendimentoEvents;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace AMDespachante.Domain.Commands.AtendimentoCommands
{
    public class AtendimentoCommandHandler(IAtendimentoRepository repository) : CommandHandler,
        IRequestHandler<NovoAtendimentoCommand, ValidationResult>,
        IRequestHandler<AtualizarAtendimentoCommand, ValidationResult>,
        IRequestHandler<RemoverAtendimentoCommand, ValidationResult>
    {
        private readonly IAtendimentoRepository _repository = repository;

        public async Task<ValidationResult> Handle(NovoAtendimentoCommand message, CancellationToken cancellationToken)
        {
            try
            {
                _validationResult.Errors.Clear();

                if (!message.IsValid()) return message.ValidationResult;

                var atendimento = new Atendimento(
                    message.Data, 
                    message.Servico, 
                    message.ValorEntrada, 
                    message.ValorSaida, 
                    message.FormaPagamento, 
                    message.Observacoes, 
                    message.EstaPago,
                    message.Status,
                    message.ClienteId, 
                    message.VeiculoId, 
                    message.NumeroATPV,
                    message.NumeroCRLV);

                _repository.Add(atendimento);

                atendimento.AddEvent(new AtendimentoCriadoEvent(atendimento));

                return await Commit(_repository.UnitOfWork);
            }
            finally
            {
                _repository.UnitOfWork.Reset();
            }
        }

        public async Task<ValidationResult> Handle(AtualizarAtendimentoCommand message, CancellationToken cancellationToken)
        {
            try
            {
                _validationResult.Errors.Clear();

                if (!message.IsValid()) return message.ValidationResult;

                var atendimento = await _repository.GetById(message.Id);

                if (atendimento is null)
                {
                    AddError("Atendimento não encontrado");
                    return _validationResult;
                }

                atendimento.Data = message.Data;
                atendimento.Servico = message.Servico;
                atendimento.ClienteId = message.ClienteId;
                atendimento.VeiculoId = message.VeiculoId;
                atendimento.ValorEntrada = message.ValorEntrada;
                atendimento.ValorSaida = message.ValorSaida;
                atendimento.FormaPagamento = message.FormaPagamento;
                atendimento.Status = message.Status;
                atendimento.NumeroATPV = message.NumeroATPV;
                atendimento.NumeroCRLV = message.NumeroCRLV;

                _repository.Update(atendimento);

                atendimento.AddEvent(new AtendimentoAtualizadoEvent(atendimento));

                return await Commit(_repository.UnitOfWork);
            }
            finally
            {
                _repository.UnitOfWork.Reset();
            }
        }

        public async Task<ValidationResult> Handle(RemoverAtendimentoCommand message, CancellationToken cancellationToken)
        {
            try
            {
                _validationResult.Errors.Clear();

                if (!message.IsValid()) return message.ValidationResult;

                var atendimento = await _repository.GetById(message.Id);

                if (atendimento is null)
                {
                    AddError("Atendimento não encontrado");
                    return _validationResult;
                }

                _repository.Delete(atendimento);

                atendimento.AddEvent(new AtendimentoRemovidoEvent(atendimento.Id));

                return await Commit(_repository.UnitOfWork);
            }
            finally
            {
                _repository.UnitOfWork.Reset();
            }
        }
    }
}
