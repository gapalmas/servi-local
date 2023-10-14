using App.Core.Entities;
using App.Core.Interfaces;

namespace App.Core.Services
{
    public class ServiceFactory : IServiceFactory
    {
        protected readonly IOperationService<Provider> operationServiceProvider;
        protected readonly IOperationService<User> operationServiceUser;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMongoRepository<Provider> mongoRepositoryProvider;
        protected readonly IMongoRepository<User> mongoRepositoryUser;

        public ServiceFactory(IOperationService<Provider> operationServiceProvider, IUnitOfWork unitOfWork, IMongoRepository<Provider> mongoRepositoryProvider, IOperationService<User> operationServiceUser, IMongoRepository<User> mongoRepositoryUser)
        {
            this.operationServiceProvider = operationServiceProvider;
            this.unitOfWork = unitOfWork;
            this.mongoRepositoryProvider = mongoRepositoryProvider;
            this.operationServiceUser = operationServiceUser;
            this.mongoRepositoryUser = mongoRepositoryUser;
        }

        public IOperationService<Provider> OperationServiceProvider => operationServiceProvider ?? new OperationService<Provider>(mongoRepositoryProvider);
        public IOperationService<User> OperationServiceUser => operationServiceUser ?? new OperationService<User>(mongoRepositoryUser);
    }
}
