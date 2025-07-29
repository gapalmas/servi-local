using App.Core.Dto.Request.Provider;
using App.Core.Dto.Request.User;
using App.Core.Entities;
using AutoMapper;

namespace App.Core.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserRequestDto>().ReverseMap();
            CreateMap<Provider, ProviderRequestDto>().ReverseMap();
        }
    }
}