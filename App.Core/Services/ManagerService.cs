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
        protected readonly IGenericRepository<Provider> RepositoryProvider;
        protected readonly IGenericRepository<User> RepositoryUser;

        public ManagerService(IGenericService<Provider> ServiceProvider, IUnitOfWork unitOfWork, IGenericRepository<Provider> RepositoryProvider, IGenericService<User> ServiceUser, IGenericRepository<User> RepositoryUser)
        {
            this.ServiceProvider = ServiceProvider;
            this.unitOfWork = unitOfWork;
            this.RepositoryProvider = RepositoryProvider;
            this.ServiceUser = ServiceUser;
            this.RepositoryUser = RepositoryUser;
        }

        IGenericService<Provider> IManagerService.ServiceProvider => ServiceProvider ?? new RepositoryService<Provider>(RepositoryProvider);

        IGenericService<User> IManagerService.ServiceUser => ServiceUser ?? new RepositoryService<User>(RepositoryUser);
    }
}
