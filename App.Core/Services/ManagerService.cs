using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Infrastructure;

namespace App.Core.Services
{
    public class ManagerService : IManagerService
    {
        public IGenericService<Provider> ServiceProvider { get; }
        public IGenericService<User> ServiceUser { get; }
        public IGenericService<INE> ServiceIne { get; }
        private readonly IUnitOfWork UnitOfWork;

        public ManagerService(
            IGenericService<Provider> serviceProvider,
            IGenericService<User> serviceUser,
            IGenericService<INE> serviceIne,
            IUnitOfWork unitOfWork)
        {
            ServiceProvider = serviceProvider;
            ServiceUser = serviceUser;
            ServiceIne = serviceIne;
            UnitOfWork = unitOfWork;
        }
    }
}