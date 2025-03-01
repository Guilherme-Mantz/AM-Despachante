using AMDespachante.Infra.Identity.DTOs;

namespace AMDespachante.Infra.Identity.Interfaces
{
    public interface IIdentityManagementService
    {
        Task CreateUser(UserDto userDto);
        Task UpdateUser(UserDto userDto);
        Task RemoveUser(string cpf);
    }
}
