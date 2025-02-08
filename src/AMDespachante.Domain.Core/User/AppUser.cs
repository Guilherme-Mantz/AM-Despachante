using Microsoft.AspNetCore.Http;

namespace AMDespachante.Domain.Core.User
{
    public class AppUser : IAppUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AppUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private string name;

        public string Name
        {
            get { return _accessor.HttpContext.User.Identity.Name; }

        }

        public string? GetUserEmail() => this.IsAuthenticated() ? _accessor.HttpContext?.User.GetUserEmail() : string.Empty;

        public string GetUserName()
        {
            throw new NotImplementedException();
        }

        public string GetUserInitials()
        {
            throw new NotImplementedException();
        }

        public string? GetUserId() => this.IsAuthenticated() ? _accessor.HttpContext?.User.GetUserId() : string.Empty;

        public bool IsSysAdmin()
        {
            throw new NotImplementedException();
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
