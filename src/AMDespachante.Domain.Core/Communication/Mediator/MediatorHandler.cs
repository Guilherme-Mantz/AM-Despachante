using AMDespachante.Domain.Core.Data.EventSourcing;
using AMDespachante.Domain.Core.Message;
using FluentValidation.Results;
using MediatR;

namespace AMDespachante.Domain.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public MediatorHandler(IMediator mediator,
                               IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }
        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);

            _eventStore.Store(@event);
        }
        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

    }
}
