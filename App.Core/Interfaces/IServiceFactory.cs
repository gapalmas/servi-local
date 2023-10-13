using App.Core.Entities;

namespace App.Core.Interfaces
{
    public interface IServiceFactory
    {
        IOperationService<Provider> OperationServiceProvider { get; }
    }
}
