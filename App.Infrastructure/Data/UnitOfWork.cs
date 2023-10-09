using App.Core.Interfaces;
using MongoDB.Driver;

namespace App.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoDatabase mongoDatabase;

        public UnitOfWork(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return true;
        }

        //public void Dispose()
        //{
        // Dispose if necessary but mongo w/o state 
        //}
    }
}
