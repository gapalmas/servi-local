using App.Core.AutoMapper;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using App.Core.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
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
            services.AddSwagger();
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
            services.AddScoped<IManagerGenericService, ManagerGenericService>();
            services.AddScoped<IGenericService<Provider>, GenericService<Provider>>();
            services.AddScoped<IGenericService<User>, GenericService<User>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IServiceFactory, ServiceFactory>();

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Services API Local", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        private static IServiceCollection AddServiceAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""))
                };
            });

            //var keyBytes = Encoding.UTF8.GetBytes(configuration["AppSettings:Token"] ?? "");

            //services.AddAuthentication(config =>
            //{

            //    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(config =>
            //{
            //    config.RequireHttpsMetadata = false;
            //    config.SaveToken = false;
            //    config.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});


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
