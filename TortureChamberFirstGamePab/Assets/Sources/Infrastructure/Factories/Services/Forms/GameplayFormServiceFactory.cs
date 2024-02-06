using System;
using Sources.Controllers.Forms.Gameplays;
using Sources.Domain.Datas.Players;
using Sources.Infrastructure.Factories.Controllers.Forms.Gameplays;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.Forms;
using Sources.InfrastructureInterfaces.Services.Forms;
using Sources.Presentation.Views.Forms.Common;
using Sources.Presentation.Views.Forms.Gameplays;
using Sources.Presentation.Voids;
using Sources.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Services.Forms
{
    public class GameplayFormServiceFactory
    {
        //TODO порефакторить иерархию ресурсов и папок
        private readonly FormService _formService;
        private readonly HudFormPresenterFactory _hudFormPresenterFactory;
        private readonly PauseMenuFormPresenterFactory _pauseMenuFormPresenterFactory;
        private readonly UpgradeFormPresenterFactory _upgradeFormPresenterFactory;
        private readonly PlayerUpgradeViewFactory _playerUpgradeViewFactory;
        private readonly ButtonUIFactory _buttonUIFactory;

        public GameplayFormServiceFactory
        (
            FormService formService,
            HudFormPresenterFactory hudFormPresenterFactory,
            PauseMenuFormPresenterFactory pauseMenuFormPresenterFactory,
            UpgradeFormPresenterFactory upgradeFormPresenterFactory,
            PlayerUpgradeViewFactory playerUpgradeViewFactory,
            ButtonUIFactory buttonUIFactory
        )
        {
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
        }

        public IFormService Create(PlayerUpgrade playerUpgrade, Player player,  HUD hud)
        {
            Form<HudFormView, HudFormPresenter> hudForm = new Form<HudFormView, HudFormPresenter>(
                _hudFormPresenterFactory.Create, hud.GameplayFormsContainer.HudFormView);
            
            _formService.Add(hudForm);

            Form<PauseMenuFormView, PauseMenuFormPresenter> pauseForm = 
                new Form<PauseMenuFormView, PauseMenuFormPresenter>(
                _pauseMenuFormPresenterFactory.Create, hud.GameplayFormsContainer.PauseMenuFormView);
            
            _formService.Add(pauseForm);

            //TODO зарегать эту формочку на триггер
            Form<UpgradeFormView, UpgradeFormPresenter> upgradeForm = 
                new Form<UpgradeFormView, UpgradeFormPresenter>(
                _upgradeFormPresenterFactory.Create, hud.GameplayFormsContainer.UpgradeFormView);
            
            _formService.Add(upgradeForm);
            
            //TODO перетащил сюда потомучто эти вьшки с формочки апгрейда
            //PlayerUpgradeViews
            IPlayerUpgradeView playerCharismaUpgradeView = 
                _playerUpgradeViewFactory.Create(playerUpgrade.CharismaUpgrader, player.Wallet,
                    hud.PlayerUpgradeViewsContainer.CharismaUpgradeView);
            IPlayerUpgradeView playerInventoryUpgradeView =
                _playerUpgradeViewFactory.Create(playerUpgrade.InventoryUpgrader, player.Wallet,
                    hud.PlayerUpgradeViewsContainer.InventoryUpgradeView);
            IPlayerUpgradeView playerMovementUpgradeView =
                _playerUpgradeViewFactory.Create(playerUpgrade.MovementUpgrader, player.Wallet,
                    hud.PlayerUpgradeViewsContainer.MovementUpgradeView);
            
            //TavernUpgradePointButtons
            _buttonUIFactory.Create(hud.TavernUpgradePointButtons.CharismaButtonUI,
                playerCharismaUpgradeView.Upgrade);
            _buttonUIFactory.Create(hud.TavernUpgradePointButtons.InventoryButtonUI,
                playerInventoryUpgradeView.Upgrade);
            _buttonUIFactory.Create(hud.TavernUpgradePointButtons.MovementButtonUI,
                playerMovementUpgradeView.Upgrade);

            //PauseMenuButtons
            _buttonUIFactory.Create(hud.PauseMenuButton, 
                hud.GameplayFormsContainer.HudFormView.ShowPauseMenu);
            _buttonUIFactory.Create(hud.PauseMenuWindow.QuitButton, 
                () => Application.Quit());
            _buttonUIFactory.Create(hud.PauseMenuWindow.CloseButton, 
                hud.GameplayFormsContainer.PauseMenuFormView.ShowHudFormView);

            return _formService;
        }
    }
}