using AMDespachante.EventSourcing.Context;
using AMDespachante.Infra.Data.Context;
using AMDespachante.UI.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace AMDespachante.UI.Web.Configuraiton
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContextFactory<AmDespachanteContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContextFactory<EventStoreSqlContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContextFactory<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }
}
