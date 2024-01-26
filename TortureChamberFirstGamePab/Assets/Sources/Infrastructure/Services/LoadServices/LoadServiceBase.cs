using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Constants;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Players.Data;
using Sources.Domain.Players.PlayerCameras;
using Sources.Domain.Taverns.Data;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.Presentation.Voids;
using Sources.Presentation.Voids.GamePoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Players;
using Sources.Utils.Repositoryes.CollectionRepository;
using Sources.Utils.Repositoryes.ItemRepository;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Services.LoadServices
{
    public abstract class LoadServiceBase : ILoadService
    {
        protected readonly IDataService<Player> PlayerDataService;
        protected readonly IDataService<PlayerUpgrade> PlayerUpgradeDataService;
        protected readonly IDataService<Tavern> TavernDataService;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly IPrefabFactory _prefabFactory;

        private readonly HUD _hud;
        private readonly RootGamePoints _rootGamePoints;
        private readonly CollectionRepository _collectionRepository;
        private readonly EatPointViewFactory _eatPointViewFactory;
        private readonly SeatPointViewFactory _seatPointViewFactory;
        private readonly ITavernProviderSetter _tavernProviderSetter;
        private readonly IUpgradeProviderSetter _upgradeProviderSetter;
        private readonly TavernFoodPickUpPointViewFactory _tavernFoodPickUpPointViewFactory;
        private readonly TavernMoodViewFactory _tavernMoodViewFactory;
        private readonly PlayerUpgradeViewFactory _playerUpgradeViewFactory;
        private readonly DiContainer _diContainer;
        private readonly ItemProvider<IItem> _itemProvider;
        private readonly PlayerMovementViewFactory _playerMovementViewFactory;
        private readonly PlayerCameraViewFactory _playerCameraViewFactory;
        private readonly PlayerInventoryViewFactory _playerInventoryViewFactory;
        private readonly PlayerWalletViewFactory _playerWalletViewFactory;
        private readonly TextUIFactory _textUIFactory;
        private readonly ButtonUIFactory _buttonUIFactory;

        private VisitorSpawnService _visitorSpawnService;
        //TODO сделать обьект который будет хранить ссылку
        private Player _player;
        private PlayerUpgrade _playerUpgrade;
        private PlayerUpgradeService[] _playerUpgradeServices;
        private Tavern _tavern;

        protected LoadServiceBase
        (
            CollectionRepository collectionRepository,
            EatPointViewFactory eatPointViewFactory,
            SeatPointViewFactory seatPointViewFactory,
            RootGamePoints rootGamePoints,
            ITavernProviderSetter tavernProviderSetter,
            IUpgradeProviderSetter upgradeProviderSetter,
            TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory,
            TavernMoodViewFactory tavernMoodViewFactory,
            PlayerUpgradeViewFactory playerUpgradeViewFactory,
            HUD hud,
            DiContainer diContainer,
            ItemProvider<IItem> itemProvider,
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
            IPrefabFactory prefabFactory
        )
        {
            _hud = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _rootGamePoints = rootGamePoints ? rootGamePoints : 
                throw new ArgumentNullException(nameof(rootGamePoints));
            _collectionRepository = collectionRepository ?? 
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _eatPointViewFactory = eatPointViewFactory ??
                                   throw new ArgumentNullException(nameof(eatPointViewFactory));
            _seatPointViewFactory = seatPointViewFactory ??
                                    throw new ArgumentNullException(nameof(seatPointViewFactory));
            _tavernProviderSetter = tavernProviderSetter ??
                                    throw new ArgumentNullException(nameof(tavernProviderSetter));
            _upgradeProviderSetter = upgradeProviderSetter ?? 
                                     throw new ArgumentNullException(nameof(upgradeProviderSetter));
            _tavernFoodPickUpPointViewFactory = 
                tavernFoodPickUpPointViewFactory ?? 
                throw new ArgumentNullException(nameof(tavernFoodPickUpPointViewFactory));
            _tavernMoodViewFactory = tavernMoodViewFactory ?? 
                                     throw new ArgumentNullException(nameof(tavernMoodViewFactory));
            _playerUpgradeViewFactory = playerUpgradeViewFactory ?? 
                                        throw new ArgumentNullException(nameof(playerUpgradeViewFactory));
            _diContainer = diContainer;
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
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
        
        public void Load()
        {
            //TODO нормально сложить все что находится на сцене
            _player = CreatePlayer();
            _playerUpgrade = CreatePlayerUpgrade();
            _tavern = CreateTavern();
            
            //UpgradeBrokers
            //TODO сделать тишку для маркера
            //TODO сделать один сервис для всех Upgradov
            //TODO сделать три интерфейса
            //TODO или передавать только нужную информацию из Upgreyda
            _upgradeProviderSetter.SetCharisma(_playerUpgrade.CharismaUpgrader);
            _upgradeProviderSetter.SetInventory(_playerUpgrade.InventoryUpgrader);
            _upgradeProviderSetter.SetMovement(_playerUpgrade.MovementUpgrader);
            
            _tavernProviderSetter.SetTavernMood(_tavern.TavernMood);
            _tavernProviderSetter.SetGameplay(_tavern.GamePlay);
            
            //Items
            ItemConfig beerConfig = Resources.Load<ItemConfig>(Constant.ItemConfigPath.Beer);
            ItemConfig breadConfig = Resources.Load<ItemConfig>(Constant.ItemConfigPath.Bread);
            ItemConfig meatConfig = Resources.Load<ItemConfig>(Constant.ItemConfigPath.Meat);
            ItemConfig soupConfig = Resources.Load<ItemConfig>(Constant.ItemConfigPath.Soup);
            ItemConfig wineConfig = Resources.Load<ItemConfig>(Constant.ItemConfigPath.Wine);

            IItem[] items = new IItem[]
            {
                new Beer(beerConfig),
                new Bread(breadConfig),
                new Meat(meatConfig),
                new Soup(soupConfig),
                new Wine(wineConfig)
            };
            
            _itemProvider.AddCollection(items);

            //EatAndSeatGamePoints
            RootGamePoints rootGamePoints = Resolve<RootGamePoints>();
            
            List<SeatPointView> seatPoints = new List<SeatPointView>();
            
            foreach (SeatPointView seatPointView in rootGamePoints.GetComponentsInChildren<SeatPointView>())
            {
                _seatPointViewFactory.Create(seatPointView);
                _eatPointViewFactory.Create(seatPointView.EatPointView);
                seatPoints.Add(seatPointView);
            }

            List<OutDoorPoint> outDoorPoints = rootGamePoints.GetComponentsInChildren<OutDoorPoint>().ToList();
            
            _collectionRepository.Add(seatPoints);
            _collectionRepository.Add(outDoorPoints);
            
            //HudText
            _textUIFactory.Create(_hud.TextUIContainer.PlayerWalletText, _player.Wallet.Coins);
            
            //PlayerMovementView
            //TODO исправить
            PlayerView playerView = Object.FindObjectOfType<PlayerView>();
            PlayerMovementView playerMovementView = 
                _playerMovementViewFactory.Create(_player.Movement, _player.Inventory, 
                    playerView.Movement, playerView.Animation);
            
            //PlayerWalletView
            PlayerWalletView playerWalletView = 
                _playerWalletViewFactory.Create(_player.Wallet, playerView.Wallet);
            
            //PlayerCameraView
            PlayerCamera playerCamera = new PlayerCamera();
            IPlayerCameraView playerCameraView = _playerCameraViewFactory.Create(playerCamera);
            playerCameraView.SetTargetTransform(playerMovementView.Transform);
            
            //PlayerInventory
            PlayerInventoryView playerInventoryView =
                _playerInventoryViewFactory.Create(_player.Inventory, playerView.Inventory);
            
            //PlayerUpgradeViews
            //TODO не работают кнопки контейнеров
            IPlayerUpgradeView playerCharismaUpgradeView = 
                _playerUpgradeViewFactory.Create(_playerUpgrade.CharismaUpgrader, _player.Wallet,
                _hud.PlayerUpgradeViewsContainer.CharismaUpgradeView);
            IPlayerUpgradeView playerInventoryUpgradeView =
            _playerUpgradeViewFactory.Create(_playerUpgrade.InventoryUpgrader, _player.Wallet,
                _hud.PlayerUpgradeViewsContainer.InventoryUpgradeView);
            IPlayerUpgradeView playerMovementUpgradeView =
            _playerUpgradeViewFactory.Create(_playerUpgrade.MovementUpgrader, _player.Wallet,
                _hud.PlayerUpgradeViewsContainer.MovementUpgradeView);
            
            //TavernUpgradePointButtons
            _buttonUIFactory.Create(_hud.TavernUpgradePointButtons.CharismaButtonUI,
                playerCharismaUpgradeView.Upgrade);
            _buttonUIFactory.Create(_hud.TavernUpgradePointButtons.InventoryButtonUI,
                playerInventoryUpgradeView.Upgrade);
            _buttonUIFactory.Create(_hud.TavernUpgradePointButtons.MovementButtonUI,
                playerMovementUpgradeView.Upgrade);

            //TavernMood
            _imageUIFactory.Create(_hud.TavernMoodImageUI);
            _tavernMoodViewFactory.Create(_hud.TavernMoodView, _tavern.TavernMood, _hud.TavernMoodImageUI);
            
            //TavernPickUpPoints
            PickUpPointUIImages beerPickUpPointImageUI =
                _rootGamePoints.BeerPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.BeerPickUpPointView, beerPickUpPointImageUI,
                _imageUIFactory, beerConfig);

            PickUpPointUIImages breadPickUpPointImageUI =
                _rootGamePoints.BreadPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.BreadPickUpPointView, 
                breadPickUpPointImageUI, _imageUIFactory, breadConfig);
            
            PickUpPointUIImages meatPickUpPointImageUI =
                _rootGamePoints.MeatPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.MeatPickUpPointView, 
                meatPickUpPointImageUI, _imageUIFactory, meatConfig);
            
            PickUpPointUIImages soupPickUpPointImageUI =
                _rootGamePoints.SoupPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.SoupPickUpPointView, 
                soupPickUpPointImageUI, _imageUIFactory, soupConfig);

            PickUpPointUIImages winePickUpPointImageUI =
                _rootGamePoints.WinePickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(_rootGamePoints.WinePickUpPointView, 
                winePickUpPointImageUI, _imageUIFactory, wineConfig);
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

        private T Resolve<T>() => 
            _diContainer.Resolve<T>();
    }
}