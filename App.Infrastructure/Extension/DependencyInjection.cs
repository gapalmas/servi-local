using App.Core.Entities;
using App.Core.Interfaces.Infrastructure;
using App.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace App.Infrastructure.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var db = Environment.GetEnvironmentVariable("MongoDb");
            services.AddSingleton<IMongoDatabase>(_ => new MongoClient(db).GetDatabase("srv-local"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<Provider>), typeof(GenericRepository<Provider>));
            services.AddScoped(typeof(IGenericRepository<User>), typeof(GenericRepository<User>));

            return services;
        }
    }
}