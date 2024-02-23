using System;
using Agava.WebUtility;
using Sources.App.Core;
using Sources.Infrastructure.Factories.App;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
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
            _sdkInitializeService.Register();
            await _sdkInitializeService.Initialize();
            
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