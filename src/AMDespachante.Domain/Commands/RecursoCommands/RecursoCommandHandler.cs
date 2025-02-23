using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Events.RecursoEvents;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace AMDespachante.Domain.Commands.RecursoCommands;

public class RecursoCommandHandler : CommandHandler,
        IRequestHandler<NovoRecursoCommand, ValidationResult>,
        IRequestHandler<AtualizarRecursoCommand, ValidationResult>,
        IRequestHandler<RemoverRecursoCommand, ValidationResult>
{
    private readonly IRecursoRepository _recursoRepository;

    public RecursoCommandHandler(IRecursoRepository recursoRepository)
    {
        _recursoRepository = recursoRepository;
    }

    public async Task<ValidationResult> Handle(NovoRecursoCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var recurso = new Recurso(message.Nome, message.Email, message.Cpf, message.Telefone, true, true, message.Cargo);

        _recursoRepository.Add(recurso);

        recurso.AddEvent(new RecursoCriadoEvent(recurso));

        return await Commit(_recursoRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarRecursoCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var recurso = await _recursoRepository.GetById(message.Id);

        if(recurso is null)
        {
            AddError("Recurso não encontrado");
            return _validationResult;
        }

        recurso.Nome = message.Nome;
        recurso.Email = message.Email;
        recurso.Cpf = message.Cpf;
        recurso.Telefone = message.Telefone;
        recurso.Ativo = message.Ativo;
        recurso.Cargo = message.Cargo;

        _recursoRepository.Update(recurso);

        recurso.AddEvent(new RecursoAtualizadoEvent(recurso));

        return await Commit(_recursoRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoverRecursoCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var recurso = await _recursoRepository.GetById(message.Id);

        if (recurso is null)
        {
            AddError("Recurso não encontrado");
            return _validationResult;
        }

        _recursoRepository.Delete(recurso);

        recurso.AddEvent(new RecursoRemovidoEvent(recurso.Id));

        return await Commit(_recursoRepository.UnitOfWork);
    }
}
