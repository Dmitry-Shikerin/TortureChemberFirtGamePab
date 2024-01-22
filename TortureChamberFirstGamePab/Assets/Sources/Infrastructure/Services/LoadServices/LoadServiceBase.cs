using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerCameras;
using Sources.Domain.Taverns.Data;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Controllers.Taverns;
using Sources.Infrastructure.Factories.Controllers.Visitors;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.Presentation.UI;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Taverns;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids;
using Sources.Utils.ObservablePropertyes;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Services.LoadServices
{
    public abstract class LoadServiceBase : ILoadService
    {
        protected readonly IDataService<Player> PlayerDataService;
        protected readonly IDataService<PlayerUpgrade> PlayerUpgradeDataService;
        protected readonly IDataService<Tavern> TavernDataService;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly PrefabFactory _prefabFactory;

        private readonly PlayerMovementViewFactory _playerMovementViewFactory;
        private readonly PlayerCameraViewFactory _playerCameraViewFactory;
        private readonly PlayerInventoryViewFactory _playerInventoryViewFactory;
        private readonly PlayerWalletViewFactory _playerWalletViewFactory;
        private readonly TextUIFactory _textUIFactory;
        private readonly ButtonUIFactory _buttonUIFactory;

        //TODO сделать обьект который будет хранить ссылку
        private Player _player;
        private PlayerUpgrade _playerUpgrade;
        private PlayerUpgradeService[] _playerUpgradeServices;
        private Tavern _tavern;

        protected LoadServiceBase
        (
            PlayerMovementViewFactory playerMovementViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory,
            PlayerInventoryViewFactory playerInventoryViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory,
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory,
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService,
            ImageUIFactory imageUIFactory,
            PrefabFactory prefabFactory
            //TODO очень плохо
        )
        {
            _playerMovementViewFactory = playerMovementViewFactory ??
                                         throw new ArgumentNullException(nameof(playerMovementViewFactory));
            _playerCameraViewFactory = playerCameraViewFactory ??
                                       throw new ArgumentNullException(nameof(playerCameraViewFactory));
            _playerInventoryViewFactory = playerInventoryViewFactory ?? 
                                          throw new ArgumentNullException(nameof(playerInventoryViewFactory));
            _playerWalletViewFactory = playerWalletViewFactory ?? 
                                       throw new ArgumentNullException(nameof(playerWalletViewFactory));
            _textUIFactory = textUIFactory ?? throw new ArgumentNullException(nameof(textUIFactory));
            _buttonUIFactory = buttonUIFactory ??
                               throw new ArgumentNullException(nameof(buttonUIFactory));
            PlayerDataService = playerDataService;
            PlayerUpgradeDataService = playerUpgradeDataService ?? 
                                       throw new ArgumentNullException(nameof(playerUpgradeDataService));
            TavernDataService = tavernDataService ?? throw new ArgumentNullException(nameof(tavernDataService));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
        }

        public void Enter()
        {
            foreach (PlayerUpgradeService playerUpgradeService in _playerUpgradeServices)
            {
                playerUpgradeService.Start();
            }
        }

        public void Exit()
        {
            
        }

        public (Player, PlayerUpgrade, Tavern) LoadData() => 
            (CreatePlayer(), CreatePlayerUpgrade(), CreateTavern());

        public void Load()
        {
            #region Player

            //TODO млгули я этот лоад выплюнуть наружу? чтобы не прокидывать сюда кучу фабрик?
            //TODO лоадить в фыабрике сцены а вьюшки креэйтить в уже созданной сцене?
            //TODO типа такого
            // (_player, _playerUpgrade, _tavern) = LoadData();
            
            _player = CreatePlayer();
            _playerUpgrade = CreatePlayerUpgrade();
            //TODO переместить сюда зависимости для таверны
            _tavern = CreateTavern();
            
            //TODO сделать провайдеры и внедрять провайдеры в сервисы
            
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
                _playerUpgrade.CharismaUpgrader, tavernUpgradePointTextUIs.PlayerCharismaLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerCharismaPriceNextLevelUpgradeTextUI,
                _player.Wallet);

            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerMovementSpeedLevelUpgradeTextUI,
                new ObservableProperty<int>());
            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerMovementPriceNextSpeedLevelUpgradeTextUI,
                new ObservableProperty<int>());
            PlayerUpgradeService playerMovementUpgradeService = new PlayerUpgradeService(
                _playerUpgrade.MovementUpgrader, tavernUpgradePointTextUIs.PlayerMovementSpeedLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerMovementPriceNextSpeedLevelUpgradeTextUI,
                _player.Wallet);

            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerInventoryLevelUpgradeTextUI,
                new ObservableProperty<int>());
            _textUIFactory.Create(tavernUpgradePointTextUIs.PlayerInventoryPriceNextLevelUpgradeTextUI,
                new ObservableProperty<int>());
            PlayerUpgradeService playerInventoryUpgradeService = new PlayerUpgradeService(
                _playerUpgrade.InventoryUpgrader, tavernUpgradePointTextUIs.PlayerInventoryLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerInventoryPriceNextLevelUpgradeTextUI,
                _player.Wallet);


            //TODO нужно здесь сделать ентер и эксить чтобы запустить сервисы
            _playerUpgradeServices = new PlayerUpgradeService[]
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
            
            ImageUI imageUI = hud.GetComponentInChildren<ImageUI>();
            _imageUIFactory.Create(imageUI);
            TavernMoodView tavernMoodView = hud.GetComponentInChildren<TavernMoodView>();
            TavernMoodPresenterFactory tavernMoodPresenterFactory =
                new TavernMoodPresenterFactory(_playerUpgrade.CharismaUpgrader);
            TavernMoodViewFactory tavernMoodViewFactory = new TavernMoodViewFactory(tavernMoodPresenterFactory);
            tavernMoodViewFactory.Create(tavernMoodView, _tavern.TavernMood, imageUI);
            
            //VisitorCounter
            VisitorCounter visitorCounter = new VisitorCounter();
            
            //Visitor
            ObjectPool<VisitorView> visitorViewObjectPool = new ObjectPool<VisitorView>();

            // VisitorPresenterFactory visitorPresenterFactory = new VisitorPresenterFactory(
            //     collectionRepository, productShuffleService, imageUIFactory, itemViewFactory, garbageSpawner,
            //     coinSpawner);
            // VisitorViewFactory visitorViewFactory = new VisitorViewFactory(visitorPresenterFactory,
            //     prefabFactory, visitorViewObjectPool);
            
            // VisitorSpawnService visitorSpawnService = new VisitorSpawnService(
            //     _tavern.GamePlay, visitorCounter, _prefabFactory, visitorViewObjectPool,
            //     visitorViewFactory, _tavern.TavernMood);
        }

        public void Save()
        {
            PlayerDataService.Save(_player);
            PlayerUpgradeDataService.Save(_playerUpgrade);
            TavernDataService.Save(_tavern);
        }

        protected abstract Player CreatePlayer();
        protected abstract PlayerUpgrade CreatePlayerUpgrade();
        protected abstract Tavern CreateTavern();
    }
}