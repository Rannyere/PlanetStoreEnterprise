﻿using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.JwtSigningCredentials;
using PSE.Identification.API.Data;
using PSE.Identification.API.Extensions;
using PSE.WebAPI.Core.Identification;

namespace PSE.Identification.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppTokenSettings");
            services.Configure<AppTokenSettings>(appSettingsSection);

            services.AddJwksManager(options => options.Algorithm = Algorithm.ES256)
                .PersistKeysToDatabaseStore<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString(name: "DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                //.AddErrorDescriber<IdentityMensagePortugues>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtConfiguration(configuration);

            return services;
        }
    }
}
