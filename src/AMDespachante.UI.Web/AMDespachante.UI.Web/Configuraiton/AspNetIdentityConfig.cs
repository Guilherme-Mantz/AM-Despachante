using AMDespachante.UI.Web.Components.Account;
using AMDespachante.UI.Web.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AMDespachante.UI.Web.Configuraiton
{
    public static class AspNetIdentityConfig
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            AddIdentityServices(services);
            AddAuthenticationServices(services);
            AddIdentityCoreServices(services);
        }

        private static void AddIdentityServices(IServiceCollection services)
        {
            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        }

        private static void AddAuthenticationServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
        }

        private static void AddIdentityCoreServices(IServiceCollection services)
        {
            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
        }
    }
}
