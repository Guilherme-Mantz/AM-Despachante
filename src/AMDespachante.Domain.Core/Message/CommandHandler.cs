using AMDespachante.Domain.Core.Data;
using FluentValidation.Results;

namespace AMDespachante.Domain.Core.Message
{
    public abstract class CommandHandler
    {
        protected ValidationResult _validationResult;

        public CommandHandler()
        {
            _validationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            _validationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected void AddError(string propertyName, string message)
        {
            _validationResult.Errors.Add(new ValidationFailure(propertyName, message));
        }
        protected async Task<ValidationResult> Commit(IUnitOfWork uow, string message)
        {
            if (!(await uow.Commit()))
            {
                AddError(message);
            }

            return _validationResult;
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow)
        {
            return await Commit(uow, "There was an error saving data").ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
