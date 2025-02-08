using FluentValidation.Results;
using MediatR;

namespace AMDespachante.Domain.Core.Message
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
