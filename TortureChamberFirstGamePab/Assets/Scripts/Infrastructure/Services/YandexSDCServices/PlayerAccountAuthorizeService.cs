using System;
using Agava.WebUtility;
using Agava.YandexGames;
using Scripts.InfrastructureInterfaces.Services.SDCServices;

namespace Scripts.Infrastructure.Services.YandexSDCServices
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

            if (PlayerAccount.IsAuthorized)
                return;

            PlayerAccount.Authorize(onSuccessCallback);
        }
    }
}