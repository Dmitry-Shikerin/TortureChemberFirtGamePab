using System;
using Agava.WebUtility;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class FocusService : IFocusService
    {
        private readonly IPauseService _pauseService;
        private readonly IWebGlService _webGlService;

        public FocusService
        (
            IPauseService pauseService,
            IWebGlService webGlService
        )
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _webGlService = webGlService ?? throw new ArgumentNullException(nameof(webGlService));
        }

        public void Enter(object payload = null)
        {
            if(_webGlService.IsWebGl == false)
                return;
            
            OnInBackgroundChangeWeb(WebApplication.InBackground);
            OnInBackgroundChangeApp(Application.isFocused);
            
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        public void Exit()
        {
            if(_webGlService.IsWebGl == false)
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