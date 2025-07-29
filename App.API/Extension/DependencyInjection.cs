using App.Core.AutoMapper;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;
using App.Core.Interfaces.Services;
using App.Core.Services;
using App.Infrastructure.Data;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tesseract;

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
            services.ServiceDependencyInjection(configuration);
            services.AddSwagger();
            services.AddServiceAuthentication(configuration);
            services.AddTesseractServices(configuration);

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

        private static IServiceCollection ServiceDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Register main services
            services.AddScoped<IManagerService, ManagerService>();

            // Register generic services
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(RepositoryService<>));

            // Register specific services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProviderService, ProviderService>();
            //services.AddScoped<IINEProcessorService, INEProcessorService>();
            services.AddScoped<IServiceFactory, ServiceFactory>();

            services.AddScoped<IINEProcessorService>(sp =>
            {
                var managerService = sp.GetRequiredService<IManagerService>();
                var mapper = sp.GetRequiredService<IMapper>();

                // Configura la ruta de los archivos de datos de Tesseract
                string tessdataPath = configuration["Tesseract:TessdataPath"]
                    ?? Path.Combine(AppContext.BaseDirectory, "tessdata");

                string language = configuration["Tesseract:Language"] ?? "spa";

                return new INEProcessorService(
                    managerService,
                    mapper,
                    tessdataPath,
                    language
                );
            });

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

        // Nuevo método para configurar servicios de Tesseract
        private static IServiceCollection AddTesseractServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITesseractInitializationService, TesseractInitializationService>();
            return services;
        }

        // Resto de los métodos anteriores (AddSwagger, AddServiceAuthentication) se mantienen igual

        // Nuevas interfaces y servicios para Tesseract
        public interface ITesseractInitializationService
        {
            void VerifyTesseractInstallation();
        }

        public class TesseractInitializationService : ITesseractInitializationService
        {
            private readonly ILogger<TesseractInitializationService> _logger;
            private readonly string _tessdataPath;
            private readonly string _language;

            public TesseractInitializationService(
                ILogger<TesseractInitializationService> logger,
                IConfiguration configuration)
            {
                _logger = logger;
                _tessdataPath = configuration["Tesseract:TessdataPath"]
                    ?? Path.Combine(AppContext.BaseDirectory, "tessdata");
                _language = configuration["Tesseract:Language"] ?? "spa";
            }

            public void VerifyTesseractInstallation()
            {
                try
                {
                    _logger.LogInformation($"Verificando instalación de Tesseract en {_tessdataPath}");

                    // Verificar existencia de archivos de datos
                    string languageDataPath = Path.Combine(_tessdataPath, $"{_language}.traineddata");

                    if (!Directory.Exists(_tessdataPath))
                    {
                        throw new DirectoryNotFoundException($"Directorio de datos de Tesseract no encontrado: {_tessdataPath}");
                    }

                    if (!File.Exists(languageDataPath))
                    {
                        throw new FileNotFoundException($"Archivo de lenguaje no encontrado: {languageDataPath}");
                    }

                    // Intentar inicializar el motor Tesseract
                    using var engine = new TesseractEngine(_tessdataPath, _language, EngineMode.Default);
                    _logger.LogInformation("Instalación de Tesseract verificada exitosamente");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en la verificación de instalación de Tesseract");
                    throw;
                }
            }
        }
    }
}