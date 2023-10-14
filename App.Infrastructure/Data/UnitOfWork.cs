using App.Core.Interfaces.Infrastructure;

namespace App.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public bool SaveChangesAsync() => true;
        
    }
}