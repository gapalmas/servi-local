using App.Core.Entities;
using App.Core.Interfaces;
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
            services.AddScoped(typeof(IMongoRepository<Provider>), typeof(MongoRepository<Provider>));

            return services;
        }
    };
}
