using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerCameras;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using Sources.Presentation.Voids;
using Sources.Utils.ObservablePropertyes;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Services.LoadServices
{
    public abstract class LoadServiceBase : ILoadService
    {
        private readonly PlayerMovementViewFactory _playerMovementViewFactory;
        private readonly PlayerCameraViewFactory _playerCameraViewFactory;
        private readonly PlayerInventoryViewFactory _playerInventoryViewFactory;
        private readonly PlayerWalletViewFactory _playerWalletViewFactory;
        private readonly TextUIFactory _textUIFactory;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly IUpgradeble _playerCharismaUpgrader;
        private readonly IUpgradeble _playerMovementUpgrader;
        private readonly IUpgradeble _playerInventoryUpgrader;

        private Player _player;

        protected LoadServiceBase
        (
            PlayerMovementViewFactory playerMovementViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory,
            PlayerInventoryViewFactory playerInventoryViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory,
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory,
            //TODO очень плохо
            IUpgradeble playerCharismaUpgrader,
            IUpgradeble playerMovementUpgrader,
            IUpgradeble playerInventoryUpgrader
        )
        {
            _playerMovementViewFactory = playerMovementViewFactory ??
                                         throw new ArgumentNullException(nameof(playerMovementViewFactory));
            _playerCameraViewFactory = playerCameraViewFactory ??
                                       throw new ArgumentNullException(nameof(playerCameraViewFactory));
            _playerInventoryViewFactory = playerInventoryViewFactory ?? throw new ArgumentNullException(nameof(playerInventoryViewFactory));
            _playerWalletViewFactory = playerWalletViewFactory ?? throw new ArgumentNullException(nameof(playerWalletViewFactory));
            _textUIFactory = textUIFactory ?? throw new ArgumentNullException(nameof(textUIFactory));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _playerCharismaUpgrader = playerCharismaUpgrader;
            _playerMovementUpgrader = playerMovementUpgrader;
            _playerInventoryUpgrader = playerInventoryUpgrader;
        }

        public Player Load()
        {
            #region Player

            _player = CreatePlayer();
            
            PlayerView playerView = Object.FindObjectOfType<PlayerView>();

            HUD hud = Object.FindObjectOfType<HUD>();
            HudTextUIContainer hudTextUIContainer = hud.GetComponent<HudTextUIContainer>();
            
            _textUIFactory.Create(hudTextUIContainer.PlayerWalletText, _player.Wallet.Coins);
            
            PlayerMovementView playerMovementView = 
                _playerMovementViewFactory.Create(_player.Movement, _player.Inventory, 
                    playerView.Movement, playerView.Animation);

            PlayerInventoryView playerInventoryView =
                _playerInventoryViewFactory.Create(_player.Inventory, playerView.Inventory);
            
            PlayerWalletView playerWalletView = 
                _playerWalletViewFactory.Create(_player.Wallet, playerView.Wallet);
            
            PlayerCamera playerCamera = new PlayerCamera();
            IPlayerCameraView playerCameraView = _playerCameraViewFactory.Create(playerCamera);
            playerCameraView.SetTargetTransform(playerMovementView.Transform);
            
            playerInventoryView.PlayerInventorySlots[1].BackgroundImage.HideImage();
            playerInventoryView.PlayerInventorySlots[1].Image.HideImage();
            playerInventoryView.PlayerInventorySlots[2].BackgroundImage.HideImage();
            playerInventoryView.PlayerInventorySlots[2].Image.HideImage();
            #endregion

            TavernUpgradePointTextUIs tavernUpgradePointTextUIs =
                Object.FindObjectOfType<TavernUpgradePointTextUIs>(true);
            
            //TODO не забыть построить все презенторы для текстов
            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerCharismaLevelUpgradeTextUI,
                new ObservableProperty<int>());
            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerCharismaPriceNextLevelUpgradeTextUI,
                new ObservableProperty<int>());
            PlayerUpgradeService playerCharismaUpgradeService = new PlayerUpgradeService(
                _playerCharismaUpgrader, tavernUpgradePointTextUIs.PlayerCharismaLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerCharismaPriceNextLevelUpgradeTextUI,
                _player.Wallet);

            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerMovementSpeedLevelUpgradeTextUI,
                new ObservableProperty<int>());
            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerMovementPriceNextSpeedLevelUpgradeTextUI,
                new ObservableProperty<int>());
            PlayerUpgradeService playerMovementUpgradeService = new PlayerUpgradeService(
                _playerMovementUpgrader, tavernUpgradePointTextUIs.PlayerMovementSpeedLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerMovementPriceNextSpeedLevelUpgradeTextUI,
                _player.Wallet);

            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerInventoryLevelUpgradeTextUI,
                new ObservableProperty<int>());
            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerInventoryPriceNextLevelUpgradeTextUI,
                new ObservableProperty<int>());
            PlayerUpgradeService playerInventoryUpgradeService = new PlayerUpgradeService(
                _playerInventoryUpgrader, tavernUpgradePointTextUIs.PlayerInventoryLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerInventoryPriceNextLevelUpgradeTextUI,
                _player.Wallet);


            //TODO нужно здесь сделать ентер и эксить чтобы запустить сервисы
            PlayerUpgradeService[] playerUpgradeServices = new PlayerUpgradeService[]
            {
                playerCharismaUpgradeService,
                playerMovementUpgradeService,
                playerInventoryUpgradeService
            };

            TavernUpgradePointButtons tavernUpgradePointButtons =
                Object.FindObjectOfType<TavernUpgradePointButtons>(true);
            
            //TavernUpgradePointButtons
            _buttonUIFactory.Create(tavernUpgradePointButtons.CharismaButtonUI,
                playerCharismaUpgradeService.Upgrade);
            
            _buttonUIFactory.Create(tavernUpgradePointButtons.InventoryButtonUI,
                playerInventoryUpgradeService.Upgrade);
            
            _buttonUIFactory.Create(tavernUpgradePointButtons.MovementButtonUI,
                playerMovementUpgradeService.Upgrade);

            return _player;
        }

        public void Save()
        {
            
        }

        protected abstract Player CreatePlayer();
    }
}