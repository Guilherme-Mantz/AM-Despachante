using AMDespachante.Domain.Core.Data.EventSourcing;
using AMDespachante.EventSourcing.Context;
using AMDespachante.EventSourcing;
using AMDespachante.Infra.Data.Context;
using AMDespachante.Domain.Core.Communication.Mediator;
using AMDespachante.Domain.Core.User;
using AMDespachante.Application.AutoMapper;
using AMDespachante.Application.Interfaces;
using AMDespachante.Application.Services;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Infra.Data.Repository;
using AMDespachante.Domain.Events.RecursoEvents;
using MediatR;
using AMDespachante.Domain.Commands.RecursoCommands;
using FluentValidation.Results;
using AMDespachante.Infra.Identity.Implementations;
using AMDespachante.Infra.Identity.Interfaces;

namespace AMDespachante.UI.Web.Configuraiton
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //UI

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAppUser, AppUser>();

            // MediatR
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // AutoMapper Settings
            services.AddAutoMapper(typeof(AutomaperConfig));

            //Services
            services.AddScoped<IRecursoAppService, RecursoAppService>();

            //Events
            services.AddScoped<INotificationHandler<RecursoCriadoEvent>, RecursoEventHandler>();
            services.AddScoped<INotificationHandler<RecursoAtualizadoEvent>, RecursoEventHandler>();
            services.AddScoped<INotificationHandler<RecursoRemovidoEvent>, RecursoEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<NovoRecursoCommand, ValidationResult>, RecursoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarRecursoCommand, ValidationResult>, RecursoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverRecursoCommand, ValidationResult>, RecursoCommandHandler>();
            services.AddScoped<IRequestHandler<DesativarPrimeiroAcessoRecursoCommand, ValidationResult>, RecursoCommandHandler>();

            //Repostories
            services.AddScoped<AmDespachanteContext>();
            services.AddScoped<IRecursoRepository, RecursoRepository>();

            // Event Sourcing
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();

            // Identity
            services.AddScoped<IIdentityManagementService, IdentityManagementService>();

            return services;
        }
    }
}
