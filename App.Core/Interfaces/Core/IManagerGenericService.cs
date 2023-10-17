using App.Core.Entities;

namespace App.Core.Interfaces.Core
{
    public interface IManagerGenericService
    {
        IGenericService<Provider> GenericServiceProvider { get; }
        IGenericService<User> GenericServiceUser { get; }
    }
}
