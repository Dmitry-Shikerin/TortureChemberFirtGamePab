using Agava.WebUtility;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using InfrastructureInterfaces.Services.SDCServices;

namespace Infrastructure.Services.YandexSDCServices
{
    public class SDKInitializeService : IInitializeService
    {
        public void GameReady()
        {
            // Yandex integration is disabled for the itch.io release.
            // if (WebApplication.IsRunningOnWebGL == false)
            //     return;
            //
            // YandexGamesSdk.GameReady();
            //
        }

        public void EnableCallbackLogging()
        {
            // Yandex integration is disabled for the itch.io release.
            // if (WebApplication.IsRunningOnWebGL == false)
            //     return;
            //
            // if (YandexGamesSdk.CallbackLogging)
            //     return;
            //
            // YandexGamesSdk.CallbackLogging = true;
            //
        }

        public UniTask Initialize()
        {
            // Yandex integration is disabled for the itch.io release.
            // if (WebApplication.IsRunningOnWebGL == false)
            //     return;
            //
            // if (YandexGamesSdk.IsInitialized)
            //     return;
            //
            // await YandexGamesSdk.Initialize();
            //

            return UniTask.CompletedTask;
        }
    }
}