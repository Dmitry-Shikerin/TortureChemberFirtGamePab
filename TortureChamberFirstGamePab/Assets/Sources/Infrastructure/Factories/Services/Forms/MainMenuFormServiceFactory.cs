using System;
using Agava.YandexGames;
using Sources.Controllers.Forms.MainMenus;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Players;
using Sources.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.Forms;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.Common;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.UI;

namespace Sources.Infrastructure.Factories.Services.Forms
{
    public class MainMenuFormServiceFactory
    {
        private readonly MainMenuFormPresenterFactory _mainMenuFormPresenterFactory;
        private readonly FormService _formService;
        private readonly LeaderboardFormPresenterFactory _leaderboardFormPresenterFactory;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly SceneService _sceneService;
        private readonly IDataService<Player> _dataService;
        private readonly MainMenuHUD _mainMenuHUD;

        public MainMenuFormServiceFactory
        (
            MainMenuFormPresenterFactory mainMenuFormPresenterFactory,
            MainMenuHUD mainMenuHUD,
            FormService formService,
            LeaderboardFormPresenterFactory leaderboardFormPresenterFactory,
            ButtonUIFactory buttonUIFactory,
            SceneService sceneService,
            IDataService<Player> dataService
        )
        {
            _mainMenuFormPresenterFactory = mainMenuFormPresenterFactory ??
                                            throw new ArgumentNullException(nameof(mainMenuFormPresenterFactory));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _leaderboardFormPresenterFactory =
                leaderboardFormPresenterFactory ??
                throw new ArgumentNullException(nameof(leaderboardFormPresenterFactory));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _mainMenuHUD = mainMenuHUD ? mainMenuHUD : throw new ArgumentNullException(nameof(mainMenuHUD));
        }

        public IFormService Create()
        {
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

            //TODO после того как я делаю клеар сбиваются шрифты и выскакивают ошибки
            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.NewGameButton, async () =>
            {
                _dataService.Clear();
                await _sceneService.ChangeSceneAsync(Constant.SceneNames.Gameplay,
                    new LoadServicePayload(false));
            });

            //TODO будет ли это работать?
            //TODO исключение
            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.LeaderboardButton, () =>
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                PlayerAccount.Authorize();
                
                if(PlayerAccount.IsAuthorized)
                    PlayerAccount.RequestPersonalProfileDataPermission();
                
                if(PlayerAccount.IsAuthorized == false)
                    return;
                
                _mainMenuHUD.MainMenuFormsContainer.MainMenuFormView.ShowLeaderboard();
#endif
            });
            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.BackToMainMenuButton,
                _mainMenuHUD.MainMenuFormsContainer.LeaderboardFormView.ShowMainMenu);

            if (_dataService.CanLoad == false)
                continueGameButton.Disable();

            return _formService;
        }
    }
}