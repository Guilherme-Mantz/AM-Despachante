using AMDespachante.Domain.Core.Message;
using FluentValidation.Results;

namespace AMDespachante.Domain.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;

    }
}
