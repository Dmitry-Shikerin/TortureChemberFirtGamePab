﻿using System;
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
            OnInBackgroundChangeWeb(WebApplication.InBackground);
            OnInBackgroundChangeApp(Application.isFocused);
            
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
                _pauseService.PauseSound();
                
                return;
            }
            
            _pauseService.Continue();
            _pauseService.PauseSound();
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (isBackground)
            {
                _pauseService.Pause();
                _pauseService.PauseSound();
                
                return;
            }
            
            _pauseService.Continue();
            _pauseService.PauseSound();
        }
    }
}