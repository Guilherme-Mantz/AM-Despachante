using AMDespachante.Domain.Interfaces.Services;
using Hangfire;

namespace AMDespachante.UI.Web.Configuraiton
{
    public static class HangfireConfig
    {
        public static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(c => c
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();
        }
        public static void AddHangfireJobs(this IApplicationBuilder app)
        {
            // Configura para executar no primeiro dia de cada mês às 01:00
            // A expressão CRON "0 1 1 7-12 *" significa:
            // - 0: no minuto 0
            // - 1: na hora 1 (1:00 AM)
            // - 1: no dia 1 do mês
            // - 7-12: apenas nos meses de julho a dezembro
            // - *: em qualquer dia da semana
            RecurringJob.AddOrUpdate<ILicencaValidacaoService>(
            "validacao-veiculos-mensal",
            x => x.ProcessarValidacaoVeiculos(),
            "0 1 1 7-12 *",
            new RecurringJobOptions { TimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time") });

        }
    }
}
