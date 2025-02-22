using AMDespachante.Domain.Core.Communication.Mediator;
using AMDespachante.Domain.Core.Data;
using AMDespachante.Domain.Core.DomainObjects;
using AMDespachante.Domain.Core.Message;
using AMDespachante.Domain.Core.User;
using AMDespachante.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AMDespachante.Infra.Data.Context;

public class AmDespachanteContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IAppUser _user;

    public AmDespachanteContext(DbContextOptions<AmDespachanteContext> options, IMediatorHandler mediatorHandler, IAppUser user) : base(options)
    {
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
        _user = user;
    }

    public DbSet<Recurso> Recursos { get; set; }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Criado") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("Criado").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property("Criado").IsModified = false;
        }

        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CriadoPor") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("CriadoPor").CurrentValue = "Sys"/*_user?.Name*/;

            if (entry.State == EntityState.Modified)
                entry.Property("CriadoPor").IsModified = false;
        }

        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Modificado") != null))
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                entry.Property("Modificado").CurrentValue = DateTime.Now;
        }

        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("ModificadoPor") != null))
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                entry.Property("ModificadoPor").CurrentValue = "Sys"/*_user?.Name*/;
        }

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await SaveChangesAsync() > 0;

        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);

        return success;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("nvarchar(200)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AmDespachanteContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }
}

public static class MediatorExtension
{
    public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Events != null && x.Entity.Events.Any()).ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Events)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async domainEvent => { await mediator.PublishEvent(domainEvent); });

        await Task.WhenAll(tasks);
    }
}

