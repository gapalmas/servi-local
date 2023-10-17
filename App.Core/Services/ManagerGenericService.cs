using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;

namespace App.Core.Services
{
    public class ManagerGenericService : IManagerGenericService
    {
        protected readonly IGenericService<Provider> operationServiceProvider;
        protected readonly IGenericService<User> operationServiceUser;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMongoRepository<Provider> mongoRepositoryProvider;
        protected readonly IMongoRepository<User> mongoRepositoryUser;

        public ManagerGenericService(IGenericService<Provider> operationServiceProvider, IUnitOfWork unitOfWork, IMongoRepository<Provider> mongoRepositoryProvider, IGenericService<User> operationServiceUser, IMongoRepository<User> mongoRepositoryUser)
        {
            this.operationServiceProvider = operationServiceProvider;
            this.unitOfWork = unitOfWork;
            this.mongoRepositoryProvider = mongoRepositoryProvider;
            this.operationServiceUser = operationServiceUser;
            this.mongoRepositoryUser = mongoRepositoryUser;
        }

        public IGenericService<Provider> OperationServiceProvider => operationServiceProvider ?? new GenericService<Provider>(mongoRepositoryProvider);
        public IGenericService<User> OperationServiceUser => operationServiceUser ?? new GenericService<User>(mongoRepositoryUser);
    }
}
