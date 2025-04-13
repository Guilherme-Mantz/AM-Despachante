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
using AMDespachante.Domain.Events.ClienteEvents;
using AMDespachante.Domain.Events.VeiculoEvents;
using AMDespachante.Domain.Commands.ClienteCommands;
using AMDespachante.Domain.Commands.VeiculoCommands;
using AMDespachante.UI.Web.Services.Implementations;
using AMDespachante.UI.Web.Services.Interfaces;
using AMDespachante.Domain.Events.AtendimentoEvents;
using AMDespachante.Domain.Commands.AtendimentoCommands;

namespace AMDespachante.UI.Web.Configuraiton
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //UI
            services.AddTransient<IVeiculoService, VeiculoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAppUser, AppUser>();

            // MediatR
            services.AddTransient<IMediatorHandler, MediatorHandler>();

            // AutoMapper Settings
            services.AddAutoMapper(typeof(AutomaperConfig));

            //Services
            services.AddTransient<IAtendimentoAppService, AtendimentoAppService>();
            services.AddTransient<IRecursoAppService, RecursoAppService>();
            services.AddTransient<IClienteAppService, ClienteAppService>();
            services.AddTransient<IVeiculoAppService, VeiculoAppService>();

            //Events
            services.AddTransient<INotificationHandler<AtendimentoCriadoEvent>, AtendimentoEventHandler>();
            services.AddTransient<INotificationHandler<AtendimentoAtualizadoEvent>, AtendimentoEventHandler>();
            services.AddTransient<INotificationHandler<AtendimentoRemovidoEvent>, AtendimentoEventHandler>();

            services.AddTransient<INotificationHandler<RecursoCriadoEvent>, RecursoEventHandler>();
            services.AddTransient<INotificationHandler<RecursoAtualizadoEvent>, RecursoEventHandler>();
            services.AddTransient<INotificationHandler<RecursoRemovidoEvent>, RecursoEventHandler>();

            services.AddTransient<INotificationHandler<ClienteCriadoEvent>, ClienteEventHandler>();
            services.AddTransient<INotificationHandler<ClienteAtualizadoEvent>, ClienteEventHandler>();
            services.AddTransient<INotificationHandler<ClienteRemovidoEvent>, ClienteEventHandler>();

            services.AddTransient<INotificationHandler<VeiculoCriadoEvent>, VeiculoEventHandler>();
            services.AddTransient<INotificationHandler<VeiculoAtualizadoEvent>, VeiculoEventHandler>();
            services.AddTransient<INotificationHandler<VeiculoRemovidoEvent>, VeiculoEventHandler>();

            // Domain - Commands
            services.AddTransient<IRequestHandler<NovoAtendimentoCommand, ValidationResult>, AtendimentoCommandHandler>();
            services.AddTransient<IRequestHandler<AtualizarAtendimentoCommand, ValidationResult>, AtendimentoCommandHandler>();
            services.AddTransient<IRequestHandler<RemoverAtendimentoCommand, ValidationResult>, AtendimentoCommandHandler>();

            services.AddTransient<IRequestHandler<NovoRecursoCommand, ValidationResult>, RecursoCommandHandler>();
            services.AddTransient<IRequestHandler<AtualizarRecursoCommand, ValidationResult>, RecursoCommandHandler>();
            services.AddTransient<IRequestHandler<RemoverRecursoCommand, ValidationResult>, RecursoCommandHandler>();
            services.AddTransient<IRequestHandler<DesativarPrimeiroAcessoRecursoCommand, ValidationResult>, RecursoCommandHandler>();

            services.AddTransient<IRequestHandler<NovoClienteCommand, ValidationResult>, ClienteCommandHandler>();
            services.AddTransient<IRequestHandler<AtualizarClienteCommand, ValidationResult>, ClienteCommandHandler>();
            services.AddTransient<IRequestHandler<RemoverClienteCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddTransient<IRequestHandler<NovoVeiculoCommand, ValidationResult>, VeiculoCommandHandler>();
            services.AddTransient<IRequestHandler<AtualizarVeiculoCommand, ValidationResult>, VeiculoCommandHandler>();
            services.AddTransient<IRequestHandler<GerenciarVeiculosClienteCommand, ValidationResult>, VeiculoCommandHandler>();
            services.AddTransient<IRequestHandler<RemoverVeiculoCommand, ValidationResult>, VeiculoCommandHandler>();

            //Repostories
            services.AddTransient<AmDespachanteContext>();
            services.AddTransient<IAtendimentoRepository, AtendimentoRepository>();
            services.AddTransient<IRecursoRepository, RecursoRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IVeiculoRepository, VeiculoRepository>();

            // Event Sourcing
            services.AddTransient<IEventSourcingRepository, EventSourcingRepository>();
            services.AddTransient<IEventStore, SqlEventStore>();
            services.AddTransient<EventStoreSqlContext>();

            // Identity
            services.AddTransient<IIdentityManagementService, IdentityManagementService>();

            return services;
        }
    }
}
