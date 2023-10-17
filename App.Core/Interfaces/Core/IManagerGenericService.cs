using App.Core.Entities;
using App.Core.Interfaces.Services;

namespace App.Core.Interfaces.Core
{
    public interface IManagerGenericService
    {
        IGenericService<Provider> OperationServiceProvider { get; }
        IGenericService<User> OperationServiceUser { get; }
    }
}
