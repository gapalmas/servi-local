using App.Core.Dto.Request.User;

namespace App.Core.Interfaces.Services
{
    public interface IUserService
    {
        void Create(UserRequestDto userRequestDto);
    }
}