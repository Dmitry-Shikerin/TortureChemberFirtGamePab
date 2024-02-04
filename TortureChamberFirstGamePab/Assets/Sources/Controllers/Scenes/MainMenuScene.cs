using System;
using Sources.Controllers.Forms.MainMenus;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Constants;
using Sources.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.Forms;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.Presentation.Views.Forms.Common;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.UI;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        private readonly MainMenuHUD _mainMenuHUD;

        private readonly LeaderboardFormPresenterFactory _leaderboardFormPresenterFactory;
        private readonly MainMenuFormPresenterFactory _mainMenuFormPresenterFactory;
        private readonly FormService _formService;
        private readonly YandexLeaderboardInitializeService _yandexLeaderboardInitializeService;
        private readonly FocusService _focusService;
        private readonly SDKInitializeService _sdkInitializeService;
        private readonly IDataService<Domain.Players.Data.Player> _dataService;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly SceneService _sceneService;

        public MainMenuScene
        (
            LeaderboardFormPresenterFactory leaderboardFormPresenterFactory,
            MainMenuFormPresenterFactory mainMenuFormPresenterFactory, 
            FormService formService,
            YandexLeaderboardInitializeService yandexLeaderboardInitializeService,
            FocusService focusService,
            SDKInitializeService sdkInitializeService,
            MainMenuHUD hud,
            IDataService<Domain.Players.Data.Player> dataService,
            ButtonUIFactory buttonUIFactory,
            SceneService sceneService)
        {
            _mainMenuHUD = hud ? hud : throw new ArgumentNullException(nameof(hud));
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
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
        }

        public string Name { get; } = nameof(MainMenuScene);

        public void Enter(object payload)
        {
            //TODO куда это все вынести?
            //TODO сделать остальные формочки по аналогии
            Form<MainMenuFormView, MainMenuFormPresenter> mainMenuFormView = 
                new Form<MainMenuFormView, MainMenuFormPresenter>(
                _mainMenuFormPresenterFactory.Create, 
                _mainMenuHUD.MainMenuFormsContainer.MainMenuFormView);
            
            _formService.Add(mainMenuFormView);

            Form<LeaderboardFormView, LeaderboardFormPresenter> leaderboardFormView = 
                new Form<LeaderboardFormView, LeaderboardFormPresenter>(
                _leaderboardFormPresenterFactory.Create, 
                _mainMenuHUD.MainMenuFormsContainer.LeaderboardFormView);
            
            _formService.Add(leaderboardFormView);
            
            IButtonUI continueGameButton = _buttonUIFactory.Create(
                _mainMenuHUD.ButtonUIContainer.ContinueGameButton, async () =>
                    await _sceneService.ChangeSceneAsync(Constant.SceneNames.Gameplay,
                        new LoadServicePayload(true)));

            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.NewGameButton, async () =>
                await _sceneService.ChangeSceneAsync(Constant.SceneNames.Gameplay,
                    new LoadServicePayload(false)));

            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.LeaderboardButton,
                _mainMenuHUD.MainMenuFormsContainer.MainMenuFormView.ShowLeaderboard);

            if (_dataService.CanLoad == false)
                continueGameButton.Disable();

            _mainMenuHUD.Show();
            _formService.Show<MainMenuFormView>();

            //TODO вроде здесь это должно быть
            // _sdkInitializeService.OnCallGameReadyButtonClick();
            // _focusService.Enter();
            // _yandexLeaderboardInitializeService.Fill();
        }

        public void Exit()
        {
            // _focusService.Exit();
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