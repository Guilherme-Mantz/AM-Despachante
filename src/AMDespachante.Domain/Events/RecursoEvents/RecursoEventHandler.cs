using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AMDespachante.Domain.Events.RecursoEvents
{
    public class RecursoEventHandler :
        INotificationHandler<RecursoCriadoEvent>,
        INotificationHandler<RecursoAtualizadoEvent>,
        INotificationHandler<RecursoRemovidoEvent>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RecursoEventHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Handle(RecursoCriadoEvent notification, CancellationToken cancellationToken)
        {
            
            var user = new IdentityUser
            {
                UserName = notification.Recurso.Cpf,
                Email = notification.Recurso.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, "123Mud@r");

            if (!result.Succeeded)
            {
                return;
            }

            var roleExists = await _roleManager.RoleExistsAsync(notification.Recurso.Cargo.ToString());
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(notification.Recurso.Cargo.ToString()));
            }

            await _userManager.AddToRoleAsync(user, notification.Recurso.Cargo.ToString());
        }

        public async Task Handle(RecursoAtualizadoEvent notification, CancellationToken cancellationToken) 
        {
            //_userManager.FindByNameAsync(notification.Recurso.Cpf);
        }

        public Task Handle(RecursoRemovidoEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
