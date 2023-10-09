using App.Core.AutoMapper;
using App.Core.Entities;
using App.Core.Services;
using AutoMapper;
using FluentValidation;

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
            services.AddScoped<OperationService<Provider>>();

            return services;
        }
    }
}
