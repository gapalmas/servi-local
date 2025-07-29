using App.Core.Interfaces.Services;

namespace App.Core.Interfaces.Core
{
    public interface IServiceFactory
    {
        IProviderService ProviderService { get; }
        IUserService UserService { get; }
        IINEProcessorService INEProcessorService { get; }
    }
}