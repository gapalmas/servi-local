using App.Core.AutoMapper;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Core.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.API.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMap();
            services.ServiceDependencyInjection();
            services.AddServiceAuthentication(configuration);

            return services;
        }

        private static IServiceCollection AddAutoMap(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(MC =>
            {
                MC.AddProfile<AutoMapping>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        private static IServiceCollection ServiceDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IOperationService<Provider>, OperationService<Provider>>();

            return services;
        }

        private static IServiceCollection AddServiceAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("AppSettings:Token").Value;
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(config =>
            {

                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = false;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            //                .GetBytes(configuration.GetSection("secretKey").Value)),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //    });


            return services;
        }
    }
}
