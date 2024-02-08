using System.Collections;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class SDKInitializeService : IInitializeService
    {
        public void GameReady() => 
            YandexGamesSdk.GameReady();

        public void Register() => 
            YandexGamesSdk.CallbackLogging = true;

        public async UniTask Initialize() => 
            await YandexGamesSdk.Initialize();
    }
}