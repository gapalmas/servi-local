using App.Core.Dto.Request.Provider;

namespace App.Core.Interfaces.Services
{
    public interface IProviderService
    {
        void Create(ProviderRequestDto providerRequestDto);
    }
}
