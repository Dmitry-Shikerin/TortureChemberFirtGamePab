using System;
using Agava.WebUtility;
using Agava.YandexGames;
using InfrastructureInterfaces.Services.SDCServices;

namespace Infrastructure.Services.YandexSDCServices
{
    public class PlayerAccountAuthorizeService : IPlayerAccountAuthorizeService
    {
        public bool IsAuthorized()
        {
            // Yandex integration is disabled for the itch.io release.
            // if (WebApplication.IsRunningOnWebGL == false)
            //     return false;
            //
            // if (PlayerAccount.IsAuthorized == false)
            //     return false;
            //
            // return true;
            //

            return false;
        }

        public void Authorize(Action onSuccessCallback)
        {
            // Yandex integration is disabled for the itch.io release.
            // if (WebApplication.IsRunningOnWebGL == false)
            //     return;
            //
            // if (PlayerAccount.IsAuthorized)
            //     return;
            //
            // PlayerAccount.Authorize(onSuccessCallback);
            //
        }
    }
}