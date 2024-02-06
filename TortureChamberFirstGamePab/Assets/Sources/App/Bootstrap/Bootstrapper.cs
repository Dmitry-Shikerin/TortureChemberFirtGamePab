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
        private SDKInitializeService _sdkInitializeService;

        private async void Awake()
        {
            // _sdkInitializeService.Awake();
            // await _sdkInitializeService.Start();
            _appCore = FindObjectOfType<AppCore>() ?? new AppCoreFactory().Create();
        }

        [Inject]
        private void Construct(SDKInitializeService sdkInitializeService)
        {
            _sdkInitializeService = sdkInitializeService ??
                                    throw new NullReferenceException(nameof(sdkInitializeService));
        }
    }
}