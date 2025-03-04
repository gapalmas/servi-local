using App.Core.Entities;

namespace App.Core.Interfaces.Core
{
    public interface IManagerService
    {
        IGenericService<Provider> ServiceProvider { get; }
        IGenericService<User> ServiceUser { get; }
    }
}