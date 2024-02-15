﻿using System;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services.ScenServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.Presentation.Views.Forms.MainMenus;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        private readonly MainMenuHUD _mainMenuHUD;

        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly ILocalizationService _localizationService;
        private readonly LeaderboardFormPresenterFactory _leaderboardFormPresenterFactory;
        private readonly MainMenuFormPresenterFactory _mainMenuFormPresenterFactory;
        private readonly ILeaderboardInitializeService _yandexLeaderboardInitializeService;
        private readonly IFocusService _focusService;
        private readonly IInitializeService _sdkInitializeService;
        private readonly ISceneService _sceneService;
        private readonly MainMenuFormServiceFactory _mainMenuFormServiceFactory;

        public MainMenuScene
        (
            IBackgroundMusicService backgroundMusicService,
            ILocalizationService localizationService,
            LeaderboardFormPresenterFactory leaderboardFormPresenterFactory,
            MainMenuFormPresenterFactory mainMenuFormPresenterFactory,
            ILeaderboardInitializeService yandexLeaderboardInitializeService,
            IFocusService focusService,
            IInitializeService sdkInitializeService,
            MainMenuHUD hud,
            ISceneService sceneService,
            MainMenuFormServiceFactory mainMenuFormServiceFactory
        )
        {
            _mainMenuHUD = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _backgroundMusicService = backgroundMusicService ?? 
                                      throw new ArgumentNullException(nameof(backgroundMusicService));
            _localizationService = localizationService ?? 
                                   throw new ArgumentNullException(nameof(localizationService));
            _leaderboardFormPresenterFactory =
                leaderboardFormPresenterFactory ??
                throw new ArgumentNullException(nameof(leaderboardFormPresenterFactory));
            _mainMenuFormPresenterFactory =
                mainMenuFormPresenterFactory ??
                throw new ArgumentNullException(nameof(mainMenuFormPresenterFactory));
            _yandexLeaderboardInitializeService =
                yandexLeaderboardInitializeService ??
                throw new ArgumentNullException(nameof(yandexLeaderboardInitializeService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _sdkInitializeService = sdkInitializeService ??
                                    throw new ArgumentNullException(nameof(sdkInitializeService));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
            _mainMenuFormServiceFactory = mainMenuFormServiceFactory ??
                                          throw new ArgumentNullException(nameof(mainMenuFormServiceFactory));
        }

        public string Name { get; } = nameof(MainMenuScene);

        public void Enter(object payload)
        {
            //TODO как то так
            _mainMenuFormServiceFactory
                .Create()
                .Show<MainMenuFormView>();
            
            _mainMenuHUD.Show();
            
            _backgroundMusicService.Enter();

            //TODO исключение
#if UNITY_WEBGL && !UNITY_EDITOR
            _sdkInitializeService.GameReady();
            _focusService.Enter();
            _localizationService.Enter();
            _yandexLeaderboardInitializeService.Fill();
#endif
        }

        public void Exit()
        {
            // _backgroundMusicService.Exit();
            
#if UNITY_WEBGL && !UNITY_EDITOR
            // _focusService.Exit();
#endif
        }

        public void Update(float deltaTime)
        {
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }
    }
}