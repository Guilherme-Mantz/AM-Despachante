using MediatR;

namespace AMDespachante.Domain.Events.ClienteEvents
{
    public class ClienteEventHandler :
        INotificationHandler<ClienteCriadoEvent>,
        INotificationHandler<ClienteAtualizadoEvent>,
        INotificationHandler<ClienteRemovidoEvent>
    {
        public Task Handle(ClienteCriadoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(ClienteAtualizadoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(ClienteRemovidoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
