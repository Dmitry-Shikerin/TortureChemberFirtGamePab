using System;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Forms;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.Presentation.Views.Applications;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        private readonly CurtainView _curtainView;
        private readonly BackgroundMusicViewFactory _backgroundMusicViewFactory;
        private readonly IDataService<Setting> _settingDataService;
        private readonly IVolumeService _volumeService;
        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly ILocalizationService _localizationService;
        private readonly LeaderboardFormPresenterFactory _leaderboardFormPresenterFactory;
        private readonly MainMenuFormPresenterFactory _mainMenuFormPresenterFactory;
        private readonly FormService _formService;
        private readonly ILeaderboardInitializeService _yandexLeaderboardInitializeService;
        private readonly IFocusService _focusService;
        private readonly IInitializeService _sdkInitializeService;
        private readonly SceneService _sceneService;
        private readonly IDataService<Player> _dataService;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly MainMenuFormServiceFactory _mainMenuFormServiceFactory;
        private readonly IStickyService _stickyService;
        private readonly MainMenuHUD _mainMenuHUD;

        private bool _canLoad;

        public MainMenuSceneFactory
        (
            CurtainView curtainView,
            BackgroundMusicViewFactory backgroundMusicViewFactory,
            IDataService<Setting> settingDataService,
            IVolumeService volumeService,
            ILocalizationService localizationService,
            LeaderboardFormPresenterFactory leaderboardFormPresenterFactory,
            MainMenuFormPresenterFactory mainMenuFormPresenterFactory,
            FormService formService,
            ILeaderboardInitializeService yandexLeaderboardInitializeService,
            IFocusService focusService,
            IInitializeService sdkInitializeService,
            SceneService sceneService,
            IDataService<Player> dataService,
            MainMenuHUD mainMenuHUD,
            ButtonUIFactory buttonUIFactory,
            MainMenuFormServiceFactory mainMenuFormServiceFactory,
            IStickyService stickyService
        )
        {
            _curtainView = curtainView ?? throw new ArgumentNullException(nameof(curtainView));
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
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _yandexLeaderboardInitializeService =
                yandexLeaderboardInitializeService ??
                throw new ArgumentNullException(nameof(yandexLeaderboardInitializeService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _sdkInitializeService = sdkInitializeService ?? 
                                    throw new ArgumentNullException(nameof(sdkInitializeService));
            _sceneService = sceneService ??
                            throw new ArgumentNullException(nameof(sceneService));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _mainMenuFormServiceFactory = mainMenuFormServiceFactory ??
                                          throw new ArgumentNullException(nameof(mainMenuFormServiceFactory));
            _stickyService = stickyService ?? throw new ArgumentNullException(nameof(stickyService));
            _mainMenuHUD = mainMenuHUD ? mainMenuHUD : throw new ArgumentNullException(nameof(mainMenuHUD));
        }

        public async UniTask<IScene> Create(object payload)
        {
            return new MainMenuScene
            (
                _curtainView,
                _backgroundMusicViewFactory,
                _settingDataService,
                _volumeService,
                _localizationService,
                _leaderboardFormPresenterFactory,
                _mainMenuFormPresenterFactory,
                _yandexLeaderboardInitializeService,
                _focusService,
                _sdkInitializeService,
                _mainMenuHUD,
                _sceneService,
                _mainMenuFormServiceFactory,
                _stickyService
            );
        }
    }
}