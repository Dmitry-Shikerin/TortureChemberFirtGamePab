using System;
using System.Collections;
using Agava.WebUtility;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
using UnityEngine.SceneManagement;

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