using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Core.User;
using AMDespachante.Domain.Events.RecursoEvents;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace AMDespachante.Domain.Commands.RecursoCommands;

public class RecursoCommandHandler : CommandHandler,
        IRequestHandler<NovoRecursoCommand, ValidationResult>,
        IRequestHandler<AtualizarRecursoCommand, ValidationResult>,
        IRequestHandler<RemoverRecursoCommand, ValidationResult>,
        IRequestHandler<DesativarPrimeiroAcessoRecursoCommand, ValidationResult>
{
    private readonly IRecursoRepository _recursoRepository;
    private readonly IAppUser _user;

    public RecursoCommandHandler(IRecursoRepository recursoRepository, IAppUser user)
    {
        _recursoRepository = recursoRepository;
        _user = user;
    }

    public async Task<ValidationResult> Handle(NovoRecursoCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        _validationResult.Errors.Clear();

        var (emailExists, cpfExists) = await _recursoRepository.EmailOrCpfExists(message.Email, message.Cpf);

        if (emailExists) AddError("Email já cadastrado no sistema.");
        if (cpfExists) AddError("Cpf já cadastrado no sistema");

        if (emailExists || cpfExists) return _validationResult;

        var recurso = new Recurso(message.Nome, message.Email, message.Cpf, message.Telefone, true, true, message.Cargo);

        _recursoRepository.Add(recurso);

        recurso.AddEvent(new RecursoCriadoEvent(recurso));

        return await Commit(_recursoRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarRecursoCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        _validationResult.Errors.Clear();

        var recurso = await _recursoRepository.GetById(message.Id);

        if(recurso is null)
        {
            AddError("Recurso não encontrado");
            return _validationResult;
        }

        var (emailExists, cpfExists) = await _recursoRepository.EmailOrCpfExists(message.Email, message.Cpf);

        if (message.Email != recurso.Email && emailExists) AddError("Email já cadastrado no sistema.");
        if (message.Cpf != recurso.Cpf && cpfExists) AddError("Cpf já cadastrado no sistema");

        if ((emailExists && message.Email != recurso.Email) || 
            (cpfExists && message.Cpf != recurso.Cpf)) return _validationResult;

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
        
        _validationResult.Errors.Clear();

        var recurso = await _recursoRepository.GetById(message.Id);

        if (recurso is null)
        {
            AddError("Recurso não encontrado");
            return _validationResult;
        }

        if (recurso.Email == _user.GetUserEmail())
        {
            AddError("Você não pode se excluir");
            return _validationResult;
        }

        _recursoRepository.Delete(recurso);

        recurso.AddEvent(new RecursoRemovidoEvent(recurso.Id, recurso.Cpf));

        return await Commit(_recursoRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(DesativarPrimeiroAcessoRecursoCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        _validationResult.Errors.Clear();

        var recurso = await _recursoRepository.GetByCpf(message.Cpf);

        if (recurso is null)
        {
            AddError("Recurso não encontrado");
            return _validationResult;
        }

        if (!recurso.PrimeiroAcesso)
        {
            return _validationResult;
        }

        recurso.PrimeiroAcesso = false;

        _recursoRepository.Update(recurso);

        recurso.AddEvent(new RecursoAtualizadoEvent(recurso));

        return await Commit(_recursoRepository.UnitOfWork);
    }
}
