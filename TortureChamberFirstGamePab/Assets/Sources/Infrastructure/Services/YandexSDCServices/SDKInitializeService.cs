using System;
using System.Collections;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class SDKInitializeService : IInitializeService
    {
        private readonly IWebGlService _webGlService;

        public SDKInitializeService(IWebGlService webGlService)
        {
            _webGlService = webGlService ?? throw new ArgumentNullException(nameof(webGlService));
        }

        public void GameReady()
        {
            if (_webGlService.IsWebGl == false)
                return;

            YandexGamesSdk.GameReady();
        }

        public void Register()
        {
            if (_webGlService.IsWebGl == false)
                return;
            
            YandexGamesSdk.CallbackLogging = true;
        }

        public async UniTask Initialize()
        {
            if (_webGlService.IsWebGl == false)
                return;
            
            await YandexGamesSdk.Initialize();
        }
    }
}