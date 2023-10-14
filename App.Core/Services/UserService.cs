using App.Core.Dto.Request.User;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class UserService : IUserService
    {
        protected readonly IServiceFactory _serviceFactory;
        public readonly IMapper mapper;

        public UserService(IServiceFactory serviceFactory, IMapper mapper)
        {
            _serviceFactory = serviceFactory;
            this.mapper = mapper;
        }

        public void Create(UserRequestDto userRequestDto)
        {
            if (userRequestDto != null)
            {
                _serviceFactory.OperationServiceUser.InsertOneAsync(mapper.Map<User>(userRequestDto));
            }
        }
    }
}
