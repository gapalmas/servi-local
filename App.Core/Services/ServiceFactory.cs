using App.Core.Entities;
using App.Core.Interfaces;

namespace App.Core.Services
{
    public class ServiceFactory : IServiceFactory
    {
        protected readonly IOperationService<Provider> operationServiceProvider;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMongoRepository<Provider> mongoRepositoryProvider;

        public ServiceFactory(IOperationService<Provider> operationServiceProvider, IUnitOfWork unitOfWork, IMongoRepository<Provider> mongoRepositoryProvider)
        {
            this.operationServiceProvider = operationServiceProvider;
            this.unitOfWork = unitOfWork;
            this.mongoRepositoryProvider = mongoRepositoryProvider;
        }

        public IOperationService<Provider> OperationServiceProvider => operationServiceProvider ?? new OperationService<Provider>(mongoRepositoryProvider);
    }
}
