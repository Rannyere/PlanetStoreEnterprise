using Microsoft.Extensions.DependencyInjection;
using PSE.Catalog.API.Data;
using PSE.Catalog.API.Data.Repository;
using PSE.Catalog.API.Models;

namespace PSE.Catalog.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<CatalogDbContext>();
    }
}