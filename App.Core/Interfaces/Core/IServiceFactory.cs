using App.Core.Entities;

namespace App.Core.Interfaces.Core
{
    public interface IServiceFactory
    {
        IOperationService<Provider> OperationServiceProvider { get; }
        IOperationService<User> OperationServiceUser { get; }
    }
}
