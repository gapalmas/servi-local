using App.Core.Dto.Request.User;
using App.Core.Entities;
using App.Core.Interfaces.Core;
using App.Core.Interfaces.Services;
using AutoMapper;

namespace App.Core.Services
{
    public class UserService : IUserService
    {
        protected readonly IManagerService managerGenericService;
        public readonly IMapper mapper;

        public UserService(IManagerService managerGenericService, IMapper mapper)
        {
            this.managerGenericService = managerGenericService;
            this.mapper = mapper;
        }

        public void Create(UserRequestDto userRequestDto)
        {
            managerGenericService.GenericServiceUser.InsertOneAsync(mapper.Map<User>(userRequestDto));
        }

        public User GetById(string id)
        {
            return managerGenericService.GenericServiceUser.FindById(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync() 
        {
            return await managerGenericService.GenericServiceUser.GetAllAsync();
        }

        public void Update(User user)
        {
            managerGenericService.GenericServiceUser.ReplaceOne(user);
        }

        public User FindOne(UserRequestDto userRequestDto)
        {
            return managerGenericService.GenericServiceUser.FindOne(user => user.Email == userRequestDto.Email && user.Password == userRequestDto.Password);
        }

        public async Task<User> FindOneAsync(UserRequestDto userRequestDto)
        {
            return await managerGenericService.GenericServiceUser.FindOneAsync(user => user.Email == userRequestDto.Email && user.Password == userRequestDto.Password);
        }
    }
}
