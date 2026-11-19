using System;
using Agava.WebUtility;
using InfrastructureInterfaces.Services.PauseServices;
using InfrastructureInterfaces.Services.SDCServices;
using UnityEngine;

namespace Infrastructure.Services.YandexSDCServices
{
    public class FocusService : IFocusService
    {
        private readonly IPauseService _pauseService;

        private bool _isAppFocused;
        private bool _isPageInBackground;
        private bool _ownsPause;
        private bool _subscribed;

        public FocusService(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public void Enter(object payload = null)
        {
            if (WebApplication.IsRunningOnWebGL == false)
            {
                return;
            }

            if (_subscribed)
            {
                return;
            }

            _isAppFocused = Application.isFocused;
            _isPageInBackground = WebApplication.InBackground;
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
            _subscribed = true;
            UpdatePause();
        }

        public void Exit()
        {
            if (_subscribed == false)
            {
                return;
            }

            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
            _subscribed = false;
            ReleasePause();
        }

        private void OnInBackgroundChangeApp(bool isFocused)
        {
            _isAppFocused = isFocused;
            UpdatePause();
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            _isPageInBackground = isBackground;
            UpdatePause();
        }

        private void UpdatePause()
        {
            bool shouldPause = _isAppFocused == false || _isPageInBackground;

            if (shouldPause == _ownsPause)
            {
                return;
            }

            if (shouldPause)
            {
                _ownsPause = true;
                _pauseService.Pause();
                _pauseService.PauseSound();
            }
            else
            {
                ReleasePause();
            }
        }

        private void ReleasePause()
        {
            if (_ownsPause == false)
            {
                return;
            }

            _ownsPause = false;
            _pauseService.Continue();
            _pauseService.ContinueSound();
        }
    }
}
