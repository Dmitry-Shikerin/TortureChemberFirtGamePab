using System;
using Cysharp.Threading.Tasks;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Sources.Infrastructure.Payloads;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.Presentation.Views.Applications;
using Sources.Presentation.Views.Forms.MainMenus;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        private readonly BackgroundMusicViewFactory _backgroundMusicViewFactory;

        private readonly CurtainView _curtainView;
        private readonly IFocusService _focusService;
        private readonly ILocalizationService _localizationService;
        private readonly MainMenuFormServiceFactory _mainMenuFormServiceFactory;
        private readonly MainMenuHUD _mainMenuHUD;
        private readonly IInitializeService _sdkInitializeService;
        private readonly IDataService<Setting> _settingDataService;
        private readonly IStickyService _stickyService;
        private readonly IVolumeService _volumeService;
        private readonly ILeaderboardInitializeService _yandexLeaderboardInitializeService;

        public MainMenuScene(
            CurtainView curtainView,
            BackgroundMusicViewFactory backgroundMusicViewFactory,
            IDataService<Setting> settingDataService,
            IVolumeService volumeService,
            ILocalizationService localizationService,
            ILeaderboardInitializeService yandexLeaderboardInitializeService,
            IFocusService focusService,
            IInitializeService sdkInitializeService,
            MainMenuHUD hud,
            MainMenuFormServiceFactory mainMenuFormServiceFactory,
            IStickyService stickyService)
        {
            _mainMenuHUD = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _curtainView = curtainView ? curtainView : throw new ArgumentNullException(nameof(curtainView));
            _backgroundMusicViewFactory = backgroundMusicViewFactory ??
                                          throw new ArgumentNullException(nameof(backgroundMusicViewFactory));
            _settingDataService = settingDataService ?? throw new ArgumentNullException(nameof(settingDataService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _localizationService = localizationService ??
                                   throw new ArgumentNullException(nameof(localizationService));
            _yandexLeaderboardInitializeService =
                yandexLeaderboardInitializeService ??
                throw new ArgumentNullException(nameof(yandexLeaderboardInitializeService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _sdkInitializeService = sdkInitializeService ??
                                    throw new ArgumentNullException(nameof(sdkInitializeService));
            _mainMenuFormServiceFactory = mainMenuFormServiceFactory ??
                                          throw new ArgumentNullException(nameof(mainMenuFormServiceFactory));
            _stickyService = stickyService ?? throw new ArgumentNullException(nameof(stickyService));
        }

        public string Name { get; } = nameof(MainMenuScene);

        public void Enter(object payload)
        {
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

        private async void GameReady(object payload)
        {
            if (payload == null)
                return;

            if (payload is not InitializeServicePayload concrete)
                return;

            if (concrete.IsInitialized == false)
                return;

            await UniTask.WaitUntil(() => _curtainView.IsInProgress == false);

            _stickyService.ShowSticky();
            _sdkInitializeService.GameReady();
        }
    }
}