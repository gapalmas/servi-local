using App.Core.Dto.Request.User;
using App.Core.Entities;

namespace App.Core.Interfaces.Services
{
    public interface IUserService
    {
        void Create(UserRequestDto userRequestDto);
        User FindOne(UserRequestDto userRequestDto);
        Task<User> FindOneAsync(UserRequestDto userRequestDto);
        Task<IEnumerable<User>> GetAllAsync();
        User GetById(string id);
        void Update(User user);
    }
}