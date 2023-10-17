using App.Core.Dto.Request.User;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class UserService : IUserService
    {
        protected readonly IManagerGenericService managerGenericService;
        public readonly IMapper mapper;

        public UserService(IManagerGenericService managerGenericService, IMapper mapper)
        {
            this.managerGenericService = managerGenericService;
            this.mapper = mapper;
        }

        public void Create(UserRequestDto userRequestDto)
        {
            if (userRequestDto != null)
            {
                managerGenericService.GenericServiceUser.InsertOneAsync(mapper.Map<User>(userRequestDto));
            }
        }
    }
}
