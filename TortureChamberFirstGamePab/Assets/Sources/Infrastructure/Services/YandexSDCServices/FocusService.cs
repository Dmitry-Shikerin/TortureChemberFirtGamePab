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
            Debug.Log("FocusService Enter");
            if(WebApplication.IsRunningOnWebGL == false)
                return;
            
            // OnInBackgroundChangeWeb(WebApplication.InBackground);
            // OnInBackgroundChangeApp(Application.isFocused);
            //
            // Application.focusChanged += OnInBackgroundChangeApp;
            // WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
            Debug.Log("FocusService AddListeners");
        }

        public void Exit()
        {
            if(WebApplication.IsRunningOnWebGL == false)
                return;
            
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            if (inApp == false)
            {
                Debug.Log($"{nameof(OnInBackgroundChangeApp)} pause");
                _pauseService.Pause();
                _pauseService.PauseSound();
                
                return;
            }
            
            Debug.Log($"{nameof(OnInBackgroundChangeApp)} continue");
            _pauseService.Continue();
            _pauseService.ContinueSound();
        }

        private void OnInBackgroundChangeWeb(bool isBackground)
        {
            if (isBackground)
            {
                Debug.Log($"{nameof(OnInBackgroundChangeWeb)} pause");
                _pauseService.Pause();
                _pauseService.PauseSound();
                
                return;
            }

            Debug.Log($"{nameof(OnInBackgroundChangeWeb)} continue");
            _pauseService.Continue();
            _pauseService.ContinueSound();
        }
    }
}