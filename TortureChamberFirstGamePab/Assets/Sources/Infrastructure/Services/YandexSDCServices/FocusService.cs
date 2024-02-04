using System;
using Agava.WebUtility;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class FocusService
    {
        private readonly IPauseService _pauseService;

        public FocusService
        (
            IPauseService pauseService
        )
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public void Enter()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        public void Exit()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (inApp == false)
            {
                _pauseService.Pause();
                
                return;
            }
            
            _pauseService.Continue();
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (isBackground)
            {
                _pauseService.Pause();
                
                return;
            }
            
            _pauseService.Continue();
        }
    }
}