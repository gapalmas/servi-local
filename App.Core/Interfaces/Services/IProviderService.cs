using App.Core.Dto.Request.Provider;
using App.Core.Dto.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Interfaces.Services
{
    public interface IProviderService
    {
        void Create(ProviderRequestDto providerRequestDto);
    }
}
