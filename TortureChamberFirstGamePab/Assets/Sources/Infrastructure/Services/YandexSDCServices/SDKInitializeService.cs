using System.Collections;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class SDKInitializeService
    {
        public void OnCallGameReadyButtonClick() => 
            YandexGamesSdk.GameReady();

        public void Awake() => 
            YandexGamesSdk.CallbackLogging = true;

        public async UniTask Start() => 
            await YandexGamesSdk.Initialize();
    }
}