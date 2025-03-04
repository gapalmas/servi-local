namespace App.Core.Interfaces.Infrastructure
{
    public interface IUnitOfWork
    {
        bool SaveChangesAsync();
    }
}