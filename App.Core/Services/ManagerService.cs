using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;

namespace App.Core.Services
{
    public class ManagerService(IGenericService<Provider> ServiceProvider, IUnitOfWork unitOfWork, IGenericRepository<Provider> RepositoryProvider, IGenericService<User> ServiceUser, IGenericRepository<User> RepositoryUser) : IManagerService
    {
        protected readonly IGenericService<Provider> ServiceProvider = ServiceProvider;
        protected readonly IGenericService<User> ServiceUser = ServiceUser;
        protected readonly IUnitOfWork unitOfWork = unitOfWork;
        protected readonly IGenericRepository<Provider> RepositoryProvider = RepositoryProvider;
        protected readonly IGenericRepository<User> RepositoryUser = RepositoryUser;

        IGenericService<Provider> IManagerService.ServiceProvider => ServiceProvider ?? new RepositoryService<Provider>(RepositoryProvider);

        IGenericService<User> IManagerService.ServiceUser => ServiceUser ?? new RepositoryService<User>(RepositoryUser);
    }
}