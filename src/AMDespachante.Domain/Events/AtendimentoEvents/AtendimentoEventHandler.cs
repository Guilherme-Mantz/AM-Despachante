using MediatR;

namespace AMDespachante.Domain.Events.AtendimentoEvents
{
    public class AtendimentoEventHandler :
        INotificationHandler<AtendimentoCriadoEvent>,
        INotificationHandler<AtendimentoAtualizadoEvent>,
        INotificationHandler<AtendimentoRemovidoEvent>
    {
        public Task Handle(AtendimentoCriadoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(AtendimentoAtualizadoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(AtendimentoRemovidoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
