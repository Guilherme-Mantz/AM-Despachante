using AMDespachante.Infra.Identity.DTOs;
using AMDespachante.Infra.Identity.Interfaces;
using MediatR;
using System.Text.RegularExpressions;

namespace AMDespachante.Domain.Events.RecursoEvents
{
    public partial class RecursoEventHandler(IIdentityManagementService identityService) :
        INotificationHandler<RecursoCriadoEvent>,
        INotificationHandler<RecursoAtualizadoEvent>,
        INotificationHandler<RecursoRemovidoEvent>
    {
        private readonly IIdentityManagementService _identityService = identityService;

        public async Task Handle(RecursoCriadoEvent notification, CancellationToken cancellationToken)
        {

            var userDto = new UserDto
            {
                Cpf = OnlyNumbers().Replace(notification.Recurso.Cpf, ""),
                Email = notification.Recurso.Email,
                Cargo = notification.Recurso.Cargo.ToString()
            };

            await _identityService.CreateUser(userDto);
        }

        public async Task Handle(RecursoAtualizadoEvent notification, CancellationToken cancellationToken) 
        {
            var userDto = new UserDto
            {
                Cpf = OnlyNumbers().Replace(notification.Recurso.Cpf, ""),
                Email = notification.Recurso.Email,
                Cargo = notification.Recurso.Cargo.ToString()
            };

            await _identityService.UpdateUser(userDto);
        }

        public async Task Handle(RecursoRemovidoEvent notification, CancellationToken cancellationToken)
        {
            await _identityService.RemoveUser(notification.Cpf);
        }

        [GeneratedRegex(@"\D")]
        private partial Regex OnlyNumbers();
    }
}
