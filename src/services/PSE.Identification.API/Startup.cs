using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSE.Identification.API.Configuration;

namespace PSE.Identification.API;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiConfiguration(Configuration);

        services.AddIdentityConfiguration(Configuration);

        services.AddSwaggerConfiguration();

        services.AddMessageBusConfiguration(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwaggerConfiguration();

        app.UseApiConfiguration(env);
    }
}