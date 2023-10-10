namespace App.Core.Interfaces
{
    public interface IUnitOfWork
    {
        bool SaveChangesAsync();
    }
}
