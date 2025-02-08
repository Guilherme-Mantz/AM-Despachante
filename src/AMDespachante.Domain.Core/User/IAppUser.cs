namespace AMDespachante.Domain.Core.User
{
    public interface IAppUser
    {
        string Name { get; }
        string? GetUserEmail();
        string? GetUserId();
    }
}
