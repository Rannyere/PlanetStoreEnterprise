using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PSE.Core.Data;
using PSE.Core.DomainObjects;
using PSE.Core.Mediator;
using PSE.Core.Messages;
using PSE.Order.Domain.Orders;
using PSE.Order.Domain.Vouchers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PSE.Order.Infra.Data;

public class OrderDbContext : DbContext, IUnityOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public OrderDbContext(DbContextOptions<OrderDbContext> options, IMediatorHandler mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<OrderCustomer> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries()
            .Where(entry => entry.Entity.GetType().GetProperty("DateRegister") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DateRegister").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DateRegister").IsModified = false;
            }
        }

        var success = await base.SaveChangesAsync() > 0;
        if (success && _mediatorHandler != null) await _mediatorHandler.PublishEvents(this);

        return success;
    }
}

public static class MediatorExtension
{
    public static async Task PublishEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await mediator.PublishEvent(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}