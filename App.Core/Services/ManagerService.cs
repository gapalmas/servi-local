using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;

namespace App.Core.Services
{
    public class ManagerService : IManagerService
    {
        protected readonly IGenericService<Provider> ServiceProvider;
        protected readonly IGenericService<User> ServiceUser;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IRepository<Provider> RepositoryProvider;
        protected readonly IRepository<User> RepositoryUser;

        public ManagerService(IGenericService<Provider> ServiceProvider, IUnitOfWork unitOfWork, IRepository<Provider> RepositoryProvider, IGenericService<User> ServiceUser, IRepository<User> RepositoryUser)
        {
            this.ServiceProvider = ServiceProvider;
            this.unitOfWork = unitOfWork;
            this.RepositoryProvider = RepositoryProvider;
            this.ServiceUser = ServiceUser;
            this.RepositoryUser = RepositoryUser;
        }

        IGenericService<Provider> IManagerService.GenericServiceProvider => ServiceProvider ?? new RepositoryService<Provider>(RepositoryProvider);

        IGenericService<User> IManagerService.GenericServiceUser => ServiceUser ?? new RepositoryService<User>(RepositoryUser);
    }
}
