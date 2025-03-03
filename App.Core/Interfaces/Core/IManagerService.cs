using App.Core.Entities;

namespace App.Core.Interfaces.Core
{
    public interface IManagerService
    {
        IGenericService<Provider> GenericServiceProvider { get; }
        IGenericService<User> GenericServiceUser { get; }
    }
}
