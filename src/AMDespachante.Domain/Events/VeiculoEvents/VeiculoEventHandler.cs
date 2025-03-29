using MediatR;

namespace AMDespachante.Domain.Events.VeiculoEvents
{
    public class VeiculoEventHandler :
        INotificationHandler<VeiculoCriadoEvent>,
        INotificationHandler<VeiculoAtualizadoEvent>,
        INotificationHandler<VeiculoRemovidoEvent>
    {
        public Task Handle(VeiculoCriadoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(VeiculoAtualizadoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(VeiculoRemovidoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
