using System;
using Sources.Controllers.Forms;
using Sources.Controllers.Forms.MainMenus;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Infrastructure.Factories.Controllers.Forms;
using Sources.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.Forms;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.Presentation.Views.Forms;
using Sources.Presentation.Views.Forms.Common;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.UI;

namespace Sources.Infrastructure.Factories.Services.Forms
{
    public class MainMenuFormServiceFactory
    {
        private readonly AuthorizationFormPresenterFactory _authorizationFormPresenterFactory;
        private readonly SettingFormPresenterFactory _settingFormPresenterFactory;
        private readonly MainMenuFormPresenterFactory _mainMenuFormPresenterFactory;
        private readonly FormService _formService;
        private readonly LeaderboardFormPresenterFactory _leaderboardFormPresenterFactory;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly SceneService _sceneService;
        private readonly IDataService<Player> _playerDataService;
        private readonly IDataService<Tavern> _tavernDateService;
        private readonly IDataService<PlayerUpgrade> _upgradeDateService;
        private readonly IPlayerAccountAuthorizeService _playerAccountAuthorizeService;
        private readonly MainMenuHUD _mainMenuHUD;

        public MainMenuFormServiceFactory
        (
            AuthorizationFormPresenterFactory authorizationFormPresenterFactory,
            SettingFormPresenterFactory settingFormPresenterFactory,
            MainMenuFormPresenterFactory mainMenuFormPresenterFactory,
            MainMenuHUD mainMenuHUD,
            FormService formService,
            LeaderboardFormPresenterFactory leaderboardFormPresenterFactory,
            ButtonUIFactory buttonUIFactory,
            SceneService sceneService,
            IDataService<Player> playerDataService,
            IDataService<Tavern> tavernDateService,
            IDataService<PlayerUpgrade> upgradeDateService,
            IPlayerAccountAuthorizeService playerAccountAuthorizeService
        )
        {
            _authorizationFormPresenterFactory =
                authorizationFormPresenterFactory ??
                throw new ArgumentNullException(nameof(authorizationFormPresenterFactory));
            _settingFormPresenterFactory = settingFormPresenterFactory ??
                                           throw new ArgumentNullException(nameof(settingFormPresenterFactory));
            _mainMenuFormPresenterFactory = mainMenuFormPresenterFactory ??
                                            throw new ArgumentNullException(nameof(mainMenuFormPresenterFactory));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _leaderboardFormPresenterFactory =
                leaderboardFormPresenterFactory ??
                throw new ArgumentNullException(nameof(leaderboardFormPresenterFactory));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
            _tavernDateService = tavernDateService ?? throw new ArgumentNullException(nameof(tavernDateService));
            _upgradeDateService = upgradeDateService ?? throw new ArgumentNullException(nameof(upgradeDateService));
            _playerAccountAuthorizeService = playerAccountAuthorizeService ??
                                             throw new ArgumentNullException(nameof(playerAccountAuthorizeService));
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

            Form<SettingFormView, SettingFormPresenter> settingForm =
                new Form<SettingFormView, SettingFormPresenter>(
                    _settingFormPresenterFactory.Create, _mainMenuHUD.MainMenuFormsContainer.SettingFormView);

            _formService.Add(settingForm);

            Form<AuthorizationFormView, AuthorizationFormPresenter> authorizationForm =
                new Form<AuthorizationFormView, AuthorizationFormPresenter>(
                    _authorizationFormPresenterFactory.Create,
                    _mainMenuHUD.MainMenuFormsContainer.AuthorizationFormView);

            _formService.Add(authorizationForm);

            IButtonUI continueGameButton = _buttonUIFactory.Create(
                _mainMenuHUD.ButtonUIContainer.ContinueGameButton, async () =>
                    await _sceneService.ChangeSceneAsync(Constant.SceneNames.Gameplay,
                        new LoadServicePayload(true)));

            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.NewGameButton, async () =>
            {
                _playerDataService.Clear();
                _tavernDateService.Clear();
                _upgradeDateService.Clear();

                await _sceneService.ChangeSceneAsync(Constant.SceneNames.Gameplay,
                    new LoadServicePayload(false));
            });

            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.LeaderboardButton, () =>
            {
                //TODO пока есть эта проверка постоянно показывается формочка авторизации
                if (_playerAccountAuthorizeService.IsAuthorized() == false)
                {
                    _formService.Show<AuthorizationFormView>();
                    
                    return;
                }

                _mainMenuHUD.MainMenuFormsContainer.MainMenuFormView.ShowLeaderboard();
            });

            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.BackToMainMenuButton,
                _mainMenuHUD.MainMenuFormsContainer.LeaderboardFormView.ShowMainMenu);

            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.SettingButton,
                _mainMenuHUD.MainMenuFormsContainer.MainMenuFormView.ShowSetting);

            //SettingsButton
            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.SettingFormButtonContainer.BackToMainMenu,
                _mainMenuHUD.MainMenuFormsContainer.SettingFormView.BackToMainMenu<MainMenuFormView>);
            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.SettingFormButtonContainer.IncreaseVolume,
                _mainMenuHUD.MainMenuFormsContainer.SettingFormView.IncreaseVolume);
            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.SettingFormButtonContainer.TornDownVolume,
                _mainMenuHUD.MainMenuFormsContainer.SettingFormView.TurnDownVolume);

            //TODO кнопка не активна если перехожу из геймплея
            if (CanLoad() == false)
                continueGameButton.Disable();

            return _formService;
        }

        private bool CanLoad() =>
            _playerDataService.CanLoad && _tavernDateService.CanLoad && _upgradeDateService.CanLoad;
    }
}