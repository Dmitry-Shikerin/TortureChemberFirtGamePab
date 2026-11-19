using System;
using App.Core;
using Infrastructure.Factories.App;
using InfrastructureInterfaces.Services.SDCServices;
using UnityEngine;
using Zenject;

namespace App.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        private AppCore _appCore;
        private IInitializeService _sdkInitializeService;

        private async void Awake()
        {
            _sdkInitializeService.EnableCallbackLogging();

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