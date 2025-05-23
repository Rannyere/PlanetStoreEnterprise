using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSE.Cart.API.Data;
using PSE.Cart.API.Services.gRPC;
using PSE.WebAPI.Core.Identification;

namespace PSE.Cart.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CartDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();

        services.AddGrpc();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });
    }

    public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");

        app.UseAuthConfiguration();

        app.UseGrpcWeb();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcService<CartGrpcService>().RequireCors("Total").EnableGrpcWeb();
        });
    }
}