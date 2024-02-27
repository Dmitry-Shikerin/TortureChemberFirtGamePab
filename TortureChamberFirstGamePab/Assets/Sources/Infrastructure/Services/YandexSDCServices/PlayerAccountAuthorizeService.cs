using System;
using Agava.WebUtility;
using Agava.YandexGames;
using Sources.InfrastructureInterfaces.Services.SDCServices;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class PlayerAccountAuthorizeService : IPlayerAccountAuthorizeService
    {
        public bool IsAuthorized()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return false;

            if (PlayerAccount.IsAuthorized == false)
                return false;

            return true;
        }

        public void Authorize(Action onSuccessCallback)
        {            
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if(PlayerAccount.IsAuthorized)
                return;
            
            PlayerAccount.Authorize(onSuccessCallback);
        }
    }
}