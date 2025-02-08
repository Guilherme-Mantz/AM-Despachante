using AMDespachante.Domain.Core.Data.EventSourcing;
using AMDespachante.EventSourcing.Context;
using AMDespachante.EventSourcing;
using AMDespachante.Infra.Data.Context;
using AMDespachante.Domain.Core.Communication.Mediator;
using AMDespachante.Domain.Core.User;
using AMDespachante.Application.AutoMapper;

namespace AMDespachante.UI.Web.Configuraiton
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAppUser, AppUser>();

            // MediatR
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // AutoMapper Settings
            services.AddAutoMapper(typeof(AutomaperConfig));

            //Services

            //Events

            // Domain - Commands

            //Repostories
            services.AddScoped<AmDespachanteContext>();

            // Event Sourcing
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();

            return services;
        }
    }
}
