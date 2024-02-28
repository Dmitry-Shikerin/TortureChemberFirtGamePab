using System;
using Agava.WebUtility;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class FocusService : IFocusService
    {
        private readonly IPauseService _pauseService;

        public FocusService
        (
            IPauseService pauseService
        )
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public void Enter(object payload = null)
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            OnInBackgroundChangeWeb(WebApplication.InBackground);
            OnInBackgroundChangeApp(Application.isFocused);

            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        public void Exit()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (inApp == false)
            {
                _pauseService.Pause();
                _pauseService.PauseSound();

                return;
            }

            if (_pauseService.IsPaused) 
                _pauseService.Continue();

            if (_pauseService.IsSoundPaused) 
                _pauseService.ContinueSound();
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (isBackground)
            {
                _pauseService.Pause();
                _pauseService.PauseSound();

                return;
            }

            if (_pauseService.IsPaused) 
                _pauseService.Continue();

            if (_pauseService.IsSoundPaused) 
                _pauseService.ContinueSound();
        }
    }
}