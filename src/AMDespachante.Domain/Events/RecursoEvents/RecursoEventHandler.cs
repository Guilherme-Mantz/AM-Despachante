using MediatR;

namespace AMDespachante.Domain.Events.RecursoEvents
{
    public class RecursoEventHandler :
        INotificationHandler<RecursoCriadoEvent>,
        INotificationHandler<RecursoAtualizadoEvent>,
        INotificationHandler<RecursoRemovidoEvent>
    {
        public Task Handle(RecursoCriadoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(RecursoAtualizadoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(RecursoRemovidoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
