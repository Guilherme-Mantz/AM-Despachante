using AMDespachante.Infra.Identity.DTOs;
using AMDespachante.Infra.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AMDespachante.Infra.Identity.Implementations
{
    public class IdentityManagementService : IIdentityManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityManagementService> _logger;

        public IdentityManagementService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityManagementService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task CreateUser(UserDto userDto)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = userDto.Cpf,
                    Email = userDto.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, "123Mud@r");
                if (!result.Succeeded)
                {
                    _logger.LogError("Falha ao criar usuário: {errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }

                await EnsureRoleExistsAndAddUser(user, userDto.Cargo);
                _logger.LogInformation("Usuário criado com sucesso: {cpf}", userDto.Cpf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário no Identity");
            }
        }

        public async Task UpdateUser(UserDto userDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userDto.Cpf);
                if (user is null)
                {
                    _logger.LogWarning("Usuário não encontrado para atualização: {cpf}", userDto.Cpf);
                    return;
                }

                user.Email = userDto.Email;
                user.NormalizedEmail = userDto.Email.ToUpper();
                user.UserName = userDto.Cpf;
                user.NormalizedUserName = userDto.Cpf.ToUpper();

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    _logger.LogError("Falha ao atualizar usuário: {errors}",
                        string.Join(", ", updateResult.Errors.Select(e => e.Description)));
                    return;
                }

                await UpdateUserRoles(user, userDto.Cargo);
                _logger.LogInformation("Usuário atualizado com sucesso: {cpf}", userDto.Cpf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar usuário no Identity");
            }
        }

        public async Task RemoveUser(string cpf)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(cpf);
                if (user == null)
                {
                    _logger.LogWarning("Usuário não encontrado para remoção: {cpf}", cpf);
                    return;
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    _logger.LogError("Falha ao remover usuário: {errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }

                _logger.LogInformation("Usuário removido com sucesso: {cpf}", cpf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover usuário no Identity");
            }
        }

        private async Task EnsureRoleExistsAndAddUser(IdentityUser user, string role)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
                _logger.LogInformation("Role criada: {role}", role);
            }

            await _userManager.AddToRoleAsync(user, role);
        }

        private async Task UpdateUserRoles(IdentityUser user, string newRole)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles);
            }

            await EnsureRoleExistsAndAddUser(user, newRole);
        }
    }
}
