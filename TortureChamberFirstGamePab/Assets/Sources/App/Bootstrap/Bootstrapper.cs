using System;
using Sources.App.Core;
using Sources.Infrastructure.Factories.App;
using Sources.Infrastructure.Services.YandexSDCServices;
using UnityEngine;
using Zenject;

namespace Sources.App.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        private AppCore _appCore;
        private IInitializeService _sdkInitializeService;

        private async void Awake()
        {
            //TODO исключение
#if UNITY_WEBGL && !UNITY_EDITOR

            _sdkInitializeService.Register();
            await _sdkInitializeService.Initialize();
#endif
            _appCore = FindObjectOfType<AppCore>() ?? new AppCoreFactory().Create();
        }

        [Inject]
        private void Construct(IInitializeService sdkInitializeService)
        {
            _sdkInitializeService = sdkInitializeService ??
                                    throw new NullReferenceException(nameof(sdkInitializeService));
        }
    }
}