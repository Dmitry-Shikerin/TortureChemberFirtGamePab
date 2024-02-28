using System;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Forms;
using Sources.Controllers.Forms.Gameplays;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.Datas.Players;
using Sources.Infrastructure.Factories.Controllers.Forms;
using Sources.Infrastructure.Factories.Controllers.Forms.Gameplays;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.UI.AudioSources;
using Sources.Infrastructure.Services.Forms;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.Presentation.Views.Forms;
using Sources.Presentation.Views.Forms.Common;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.Presentation.Voids;
using Sources.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Services.Forms
{
    public class GameplayFormServiceFactory
    {
        private readonly TextUIFactory _textUIFactory;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly IVideoAdService _videoAdService;
        private readonly ILeaderboardScoreSetter _leaderboardScoreSetter;
        private readonly IPauseService _pauseService;
        private readonly SceneService _sceneService;
        private readonly FormService _formService;
        private readonly HudFormPresenterFactory _hudFormPresenterFactory;
        private readonly PauseMenuFormPresenterFactory _pauseMenuFormPresenterFactory;
        private readonly UpgradeFormPresenterFactory _upgradeFormPresenterFactory;
        private readonly PlayerUpgradeViewFactory _playerUpgradeViewFactory;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly AudioSourceUIFactory _audioSourceUIFactory;
        private readonly TutorialFormPresenterFactory _tutorialFormPresenterFactory;
        private readonly LoadFormPresenterFactory _loadFormPresenterFactory;
        private readonly GameOverFormPresenterFactory _gameOverFormPresenterFactory;
        private readonly SettingFormPresenterFactory _settingFormPresenterFactory;

        public GameplayFormServiceFactory
        (
            TextUIFactory textUIFactory,
            ImageUIFactory imageUIFactory,
            IVideoAdService videoAdService,
            ILeaderboardScoreSetter leaderboardScoreSetter,
            IPauseService pauseService,
            SceneService sceneService,
            FormService formService,
            HudFormPresenterFactory hudFormPresenterFactory,
            PauseMenuFormPresenterFactory pauseMenuFormPresenterFactory,
            UpgradeFormPresenterFactory upgradeFormPresenterFactory,
            PlayerUpgradeViewFactory playerUpgradeViewFactory,
            ButtonUIFactory buttonUIFactory,
            AudioSourceUIFactory audioSourceUIFactory,
            TutorialFormPresenterFactory tutorialFormPresenterFactory,
            LoadFormPresenterFactory loadFormPresenterFactory,
            GameOverFormPresenterFactory gameOverFormPresenterFactory,
            SettingFormPresenterFactory settingFormPresenterFactory
        )
        {
            _textUIFactory = textUIFactory ?? throw new ArgumentNullException(nameof(textUIFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _videoAdService = videoAdService ?? throw new ArgumentNullException(nameof(videoAdService));
            _leaderboardScoreSetter = leaderboardScoreSetter ??
                                      throw new ArgumentNullException(nameof(leaderboardScoreSetter));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
            _hudFormPresenterFactory = hudFormPresenterFactory ??
                                       throw new ArgumentNullException(nameof(hudFormPresenterFactory));
            _pauseMenuFormPresenterFactory = pauseMenuFormPresenterFactory ??
                                             throw new ArgumentNullException(nameof(pauseMenuFormPresenterFactory));
            _upgradeFormPresenterFactory = upgradeFormPresenterFactory ??
                                           throw new ArgumentNullException(nameof(upgradeFormPresenterFactory));
            _playerUpgradeViewFactory = playerUpgradeViewFactory ??
                                        throw new ArgumentNullException(nameof(playerUpgradeViewFactory));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _audioSourceUIFactory = audioSourceUIFactory ??
                                    throw new ArgumentNullException(nameof(audioSourceUIFactory));
            _tutorialFormPresenterFactory = tutorialFormPresenterFactory ??
                                            throw new ArgumentNullException(nameof(tutorialFormPresenterFactory));
            _loadFormPresenterFactory = loadFormPresenterFactory ??
                                        throw new ArgumentNullException(nameof(loadFormPresenterFactory));
            _gameOverFormPresenterFactory = gameOverFormPresenterFactory ??
                                            throw new ArgumentNullException(nameof(gameOverFormPresenterFactory));
            _settingFormPresenterFactory = settingFormPresenterFactory ?? 
                                           throw new ArgumentNullException(nameof(settingFormPresenterFactory));
        }

        public IFormService Create(PlayerUpgrade playerUpgrade, Player player,
            HUD hud, Action saveAction)
        {
            //Forms
            Form<HudFormView, HudFormPresenter> hudForm = new Form<HudFormView, HudFormPresenter>(
                _hudFormPresenterFactory.Create, hud.GameplayFormsContainer.HudFormView);

            _formService.Add(hudForm);

            Form<PauseMenuFormView, PauseMenuFormPresenter> pauseForm =
                new Form<PauseMenuFormView, PauseMenuFormPresenter>(
                    _pauseMenuFormPresenterFactory.Create, hud.GameplayFormsContainer.PauseMenuFormView);

            _formService.Add(pauseForm);

            Form<UpgradeFormView, UpgradeFormPresenter> upgradeForm =
                new Form<UpgradeFormView, UpgradeFormPresenter>(
                    _upgradeFormPresenterFactory.Create, hud.GameplayFormsContainer.UpgradeFormView);

            _formService.Add(upgradeForm);

            Form<TutorialFormView, TutorialFormPresenter> tutorialForm =
                new Form<TutorialFormView, TutorialFormPresenter>(
                    _tutorialFormPresenterFactory.Create, hud.GameplayFormsContainer.TutorialFormView);

            _formService.Add(tutorialForm);

            Form<LoadFormView, LoadFormPresenter> loadForm = new Form<LoadFormView, LoadFormPresenter>(
                _loadFormPresenterFactory.Create, hud.GameplayFormsContainer.LoadFormView);

            _formService.Add(loadForm);

            Form<GameOverFormView, GameOverFormPresenter> gameOverForm =
                new Form<GameOverFormView, GameOverFormPresenter>(
                    _gameOverFormPresenterFactory.Create, hud.GameplayFormsContainer.GameOverFormView);

            _formService.Add(gameOverForm);

            Form<SettingFormView, SettingFormPresenter> settingForm = 
                new Form<SettingFormView, SettingFormPresenter>(
                _settingFormPresenterFactory.Create, hud.GameplayFormsContainer.SettingFormView);
            
            _formService.Add(settingForm);

            //PlayerUpgradeViews
            IPlayerUpgradeView playerCharismaUpgradeView =
                _playerUpgradeViewFactory.Create(playerUpgrade.Charisma, player.Wallet,
                    hud.PlayerUpgradeViewsContainer.CharismaUpgradeView);
            IPlayerUpgradeView playerInventoryUpgradeView =
                _playerUpgradeViewFactory.Create(playerUpgrade.Inventory, player.Wallet,
                    hud.PlayerUpgradeViewsContainer.InventoryUpgradeView);
            IPlayerUpgradeView playerMovementUpgradeView =
                _playerUpgradeViewFactory.Create(playerUpgrade.Movement, player.Wallet,
                    hud.PlayerUpgradeViewsContainer.MovementUpgradeView);

            //UpgradeAudio
            _audioSourceUIFactory.Create(playerUpgrade.Charisma,
                hud.CongratulationUpgradeAudioSourceContainer.Charisma);
            _audioSourceUIFactory.Create(playerUpgrade.Inventory,
                hud.CongratulationUpgradeAudioSourceContainer.Inventory);
            _audioSourceUIFactory.Create(playerUpgrade.Movement,
                hud.CongratulationUpgradeAudioSourceContainer.Movement);

            //TavernUpgradePointButtons
            _buttonUIFactory.Create(hud.TavernUpgradePointButtons.CharismaButtonUI,
                playerCharismaUpgradeView.Upgrade);
            _buttonUIFactory.Create(hud.TavernUpgradePointButtons.InventoryButtonUI,
                playerInventoryUpgradeView.Upgrade);
            _buttonUIFactory.Create(hud.TavernUpgradePointButtons.MovementButtonUI,
                playerMovementUpgradeView.Upgrade);
            _buttonUIFactory.Create(hud.TavernUpgradePointButtons.AdvertisementButtonUI, () =>
            {
                _videoAdService.ShowVideo();
            });

            //HudButtons
            _buttonUIFactory.Create(hud.PauseMenuButton,
                hud.GameplayFormsContainer.HudFormView.ShowPauseMenu);

            //PauseMenuButtons
            _buttonUIFactory.Create(hud.PauseMenuButtonContainer.AdvertisementButton,
                _videoAdService.ShowVideo);
            _buttonUIFactory.Create(hud.PauseMenuButtonContainer.MainMenuButton, async () =>
            {
                // _pauseService.Continue();
                _formService.Show<HudFormView>();
                
                saveAction.Invoke();
                _leaderboardScoreSetter.SetPlayerScore(player.Wallet.Score.GetValue);

                await _sceneService.ChangeSceneAsync(Constant.SceneNames.MainMenu);
            });
            _buttonUIFactory.Create(hud.PauseMenuButtonContainer.SaveButton, saveAction.Invoke);
            _buttonUIFactory.Create(hud.PauseMenuButtonContainer.QuitButton, () =>
            {
                // _pauseService.Continue();
                _formService.Show<HudFormView>();
                
                saveAction.Invoke();
                _leaderboardScoreSetter.SetPlayerScore(player.Wallet.Score.GetValue);
                Application.Quit();
            });
            _buttonUIFactory.Create(hud.PauseMenuButtonContainer.CloseButton,
                hud.GameplayFormsContainer.PauseMenuFormView.ShowHudFormView);
            _buttonUIFactory.Create(hud.PauseMenuButtonContainer.TutorialButton,
                hud.GameplayFormsContainer.PauseMenuFormView.ShowTutorialFormView);
            _buttonUIFactory.Create(hud.PauseMenuButtonContainer.SettingsButton,
                hud.GameplayFormsContainer.PauseMenuFormView.ShowSettingsFormView);

            //TutorialFormButtons
            _buttonUIFactory.Create(hud.TutorialFormButtonContainer.CloseButton,
                hud.GameplayFormsContainer.TutorialFormView.ShowPauseMenu);

            //LoadFormButtons
            _buttonUIFactory.Create(hud.LoadFormButtonContainer.CloseButton,
                hud.GameplayFormsContainer.LoadFormView.ShowHudForm);

            //GameOverFormButtons
            _buttonUIFactory.Create(hud.GameOverFormButtonContainer.BackToMainMenuButton,
                async () =>
                {
                    // _pauseService.Continue();
                    //
                    _formService.Show<HudFormView>();

                    await _sceneService.ChangeSceneAsync(Constant.SceneNames.MainMenu);
                });
            
            //SettingsFormButtons
            _buttonUIFactory.Create(hud.SettingFormButtonContainer.BackToMainMenu,
                hud.GameplayFormsContainer.SettingFormView.BackToMainMenu<PauseMenuFormView>);
            _buttonUIFactory.Create(hud.SettingFormButtonContainer.IncreaseVolume,
                hud.GameplayFormsContainer.SettingFormView.IncreaseVolume);
            _buttonUIFactory.Create(hud.SettingFormButtonContainer.TornDownVolume,
                hud.GameplayFormsContainer.SettingFormView.TurnDownVolume);
            
            //UpgradeFormButton
            _buttonUIFactory.Create(hud.TavernUpgradePointButtons.CloseButtonUI, () =>
            {
                saveAction.Invoke();
                
                hud.GameplayFormsContainer.UpgradeFormView.ShowHudForm();
            });
            
            //GameOverTexts
            _textUIFactory.Create(hud.GameOverTextContainer.ScoreText, player.Wallet.Score);

            return _formService;
        }
    }
}