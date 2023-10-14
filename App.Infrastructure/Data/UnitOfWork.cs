using App.Core.Interfaces;

namespace App.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public bool SaveChangesAsync() => true;
        
    }
}