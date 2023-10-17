using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;

namespace App.Core.Services
{
    public class ManagerGenericService : IManagerGenericService
    {
        protected readonly IGenericService<Provider> GenericServiceProvider;
        protected readonly IGenericService<User> GenericServiceUser;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMongoRepository<Provider> mongoRepositoryProvider;
        protected readonly IMongoRepository<User> mongoRepositoryUser;

        public ManagerGenericService(IGenericService<Provider> GenericServiceProvider, IUnitOfWork unitOfWork, IMongoRepository<Provider> mongoRepositoryProvider, IGenericService<User> GenericServiceUser, IMongoRepository<User> mongoRepositoryUser)
        {
            this.GenericServiceProvider = GenericServiceProvider;
            this.unitOfWork = unitOfWork;
            this.mongoRepositoryProvider = mongoRepositoryProvider;
            this.GenericServiceUser = GenericServiceUser;
            this.mongoRepositoryUser = mongoRepositoryUser;
        }

        IGenericService<Provider> IManagerGenericService.GenericServiceProvider => GenericServiceProvider ?? new GenericService<Provider>(mongoRepositoryProvider);

        IGenericService<User> IManagerGenericService.GenericServiceUser => GenericServiceUser ?? new GenericService<User>(mongoRepositoryUser);
    }
}
