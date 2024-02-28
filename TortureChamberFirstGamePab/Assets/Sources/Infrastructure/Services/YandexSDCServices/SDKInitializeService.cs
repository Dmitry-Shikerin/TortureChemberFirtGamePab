using Agava.WebUtility;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class SDKInitializeService : IInitializeService
    {
        public void GameReady()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            YandexGamesSdk.GameReady();
        }

        public void Register()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;
            
            YandexGamesSdk.CallbackLogging = true;
        }

        public async UniTask Initialize()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;
            
            await YandexGamesSdk.Initialize();
        }
    }
}