using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PSE.Catalog.API.Models;
using PSE.Core.Data;
using PSE.Core.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace PSE.Catalog.API.Data;

public class CatalogDbContext : DbContext, IUnityOfWork
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Entity<Product>()
            .Property(c => c.Value)
            .HasColumnType("decimal(18,2)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}