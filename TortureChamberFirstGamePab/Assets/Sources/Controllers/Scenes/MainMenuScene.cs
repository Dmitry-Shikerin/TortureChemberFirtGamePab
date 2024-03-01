﻿using System;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Sources.Infrastructure.Payloads;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.ScenServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.Presentation.Views.Forms.MainMenus;
using UnityEngine;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        private readonly MainMenuHUD _mainMenuHUD;

        private readonly BackgroundMusicViewFactory _backgroundMusicViewFactory;
        private readonly IDataService<Setting> _settingDataService;
        private readonly IVolumeService _volumeService;
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
            BackgroundMusicViewFactory backgroundMusicViewFactory,
            IDataService<Setting> settingDataService,
            IVolumeService volumeService,
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
            _backgroundMusicViewFactory = backgroundMusicViewFactory ?? 
                                          throw new ArgumentNullException(nameof(backgroundMusicViewFactory));
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
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
            Debug.Log($"{nameof(MainMenuScene)} enter");
            
            _settingDataService.Load();
            
            _mainMenuFormServiceFactory
                .Create()
                .Show<MainMenuFormView>();
            
            _mainMenuHUD.Show();

            _backgroundMusicViewFactory.Create(_mainMenuHUD.BackgroundMusicView);
            
            _volumeService.Enter();

            _focusService.Enter();
            _localizationService.Enter();
            _yandexLeaderboardInitializeService.Fill();
            GameReady(payload);
        }

        public void Exit()
        {
            _volumeService.Exit();
            
            _focusService.Exit();
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

        private void GameReady(object payload)
        {
            if(payload == null)
                return;
            
            if(payload is not InitializeServicePayload concrete )
                return;
            
            if(concrete.IsInitialized == false)
                return;
            
            _sdkInitializeService.GameReady();
        }
    }
}