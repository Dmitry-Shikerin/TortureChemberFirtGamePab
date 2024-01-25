using System;
using System.Collections.Generic;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerCameras;
using Sources.Domain.Taverns.Data;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Infrastructure.Services.Brokers;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.Presentation.UI;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Taverns;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids;
using Sources.Presentation.Voids.GamePoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Players;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.CollectionRepository;
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
        private readonly PlayerMovementUpgradeProviderService _playerMovementUpgradeProviderService;
        private readonly PlayerInventoryUpgradeBrokerService _playerInventoryUpgradeBrokerService;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly IPrefabFactory _prefabFactory;

        private readonly HUD _hud;
        private readonly IUpgradeProviderSetter _upgradeProviderSetter;
        private readonly TavernFoodPickUpPointViewFactory _tavernFoodPickUpPointViewFactory;
        private readonly TavernMoodViewFactory _tavernMoodViewFactory;
        private readonly PlayerUpgradeViewFactory _playerUpgradeViewFactory;
        private readonly DiContainer _diContainer;
        private readonly ItemRepository<IItem> _itemRepository;
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

        //TODO сделать отдельный класс констант
        private const string BeerItemConfigPath = "Configs/Items/BeerItemConfig";
        private const string BreadItemConfigPath = "Configs/Items/BreadItemConfig";
        private const string MeatItemConfigPath = "Configs/Items/MeatItemConfig";
        private const string SoupItemConfigPath = "Configs/Items/SoupItemConfig";
        private const string WineItemConfigPath = "Configs/Items/WineItemConfig";
        
        protected LoadServiceBase
        (
            IUpgradeProviderSetter upgradeProviderSetter,
            TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory,
            TavernMoodViewFactory tavernMoodViewFactory,
            PlayerUpgradeViewFactory playerUpgradeViewFactory,
            HUD hud,
            DiContainer diContainer,
            ItemRepository<IItem> itemRepository,
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
            IPrefabFactory prefabFactory,
            PlayerMovementUpgradeProviderService playerMovementUpgradeProviderService,
            PlayerInventoryUpgradeBrokerService playerInventoryUpgradeBrokerService
            //TODO очень плохо
        )
        {
            _hud = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _upgradeProviderSetter = upgradeProviderSetter ?? throw new ArgumentNullException(nameof(upgradeProviderSetter));
            _tavernFoodPickUpPointViewFactory = tavernFoodPickUpPointViewFactory ?? throw new ArgumentNullException(nameof(tavernFoodPickUpPointViewFactory));
            _tavernMoodViewFactory = tavernMoodViewFactory ?? throw new ArgumentNullException(nameof(tavernMoodViewFactory));
            _playerUpgradeViewFactory = playerUpgradeViewFactory ?? throw new ArgumentNullException(nameof(playerUpgradeViewFactory));
            _diContainer = diContainer;
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
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
            _playerMovementUpgradeProviderService = playerMovementUpgradeProviderService;
            _playerInventoryUpgradeBrokerService = playerInventoryUpgradeBrokerService 
                                                   ?? throw new ArgumentNullException(nameof(playerInventoryUpgradeBrokerService));
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
            // _playerMovementUpgradeProviderService.Set(_playerUpgrade.MovementUpgrader);
            // _playerInventoryUpgradeBrokerService.Set(_playerUpgrade.InventoryUpgrader);
            _upgradeProviderSetter.SetCharisma(_playerUpgrade.CharismaUpgrader);
            _upgradeProviderSetter.SetInventory(_playerUpgrade.InventoryUpgrader);
            _upgradeProviderSetter.SetMovement(_playerUpgrade.MovementUpgrader);
            
            //Items
            ItemConfig beerConfig = Resources.Load<ItemConfig>(BeerItemConfigPath);
            ItemConfig breadConfig = Resources.Load<ItemConfig>(BreadItemConfigPath);
            ItemConfig meatConfig = Resources.Load<ItemConfig>(MeatItemConfigPath);
            ItemConfig soupConfig = Resources.Load<ItemConfig>(SoupItemConfigPath);
            ItemConfig wineConfig = Resources.Load<ItemConfig>(WineItemConfigPath);

            IItem[] items = new IItem[]
            {
                new Beer(beerConfig),
                new Bread(breadConfig),
                new Meat(meatConfig),
                new Soup(soupConfig),
                new Wine(wineConfig)
            };
            
            _itemRepository.AddCollection(items);

            //EatAndSeatGamePoints
            RootGamePoints rootGamePoints = Resolve<RootGamePoints>();
            
            List<SeatPointView> seatPoints = new List<SeatPointView>();
            
            //TODO переделать на линку
            foreach (SeatPointView seatPointView in rootGamePoints.GetComponentsInChildren<SeatPointView>())
            {
                //TODO плохо
                Resolve<SeatPointViewFactory>().Create(seatPointView);
                Resolve<EatPointViewFactory>().Create(seatPointView.EatPointView);
                seatPoints.Add(seatPointView);
            }

            List<OutDoorPoint> outDoorPoints = new List<OutDoorPoint>();
            
            foreach (OutDoorPoint outDoorPoint in rootGamePoints.GetComponentsInChildren<OutDoorPoint>())
            {
                outDoorPoints.Add(outDoorPoint);
            }
            
            CollectionRepository collectionRepository = Resolve<CollectionRepository>();
            collectionRepository.Add(seatPoints);
            collectionRepository.Add(outDoorPoints);
            
            //TODO сделать провайдеры и внедрять провайдеры в сервисы

            //TODO поправить
            HudTextUIContainer hudTextUIContainer = _hud.GetComponent<HudTextUIContainer>();
            
            _textUIFactory.Create(hudTextUIContainer.PlayerWalletText, _player.Wallet.Coins);
            
            //PlayerMovementView
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

            TavernUpgradePointTextUIs tavernUpgradePointTextUIs =
                Object.FindObjectOfType<TavernUpgradePointTextUIs>(true);
            
            //PlayerUpgradeViews
            PlayerUpgradeViewsContainer playerUpgradeViewsContainer =
                _hud.GetComponentInChildren<PlayerUpgradeViewsContainer>(true);

            IPlayerUpgradeView playerCharismaUpgradeView = 
                _playerUpgradeViewFactory.Create(_playerUpgrade.CharismaUpgrader, _player.Wallet,
                playerUpgradeViewsContainer.CharismaUpgradeView);
            IPlayerUpgradeView playerInventoryUpgradeView =
            _playerUpgradeViewFactory.Create(_playerUpgrade.InventoryUpgrader, _player.Wallet,
                playerUpgradeViewsContainer.InventoryUpgradeView);
            IPlayerUpgradeView playerMovementUpgradeView =
            _playerUpgradeViewFactory.Create(_playerUpgrade.MovementUpgrader, _player.Wallet,
                playerUpgradeViewsContainer.MovementUpgradeView);
            
            //TavernUpgradePointButtons
            TavernUpgradePointButtons tavernUpgradePointButtons =
                Object.FindObjectOfType<TavernUpgradePointButtons>(true);
            _buttonUIFactory.Create(tavernUpgradePointButtons.CharismaButtonUI,
                playerCharismaUpgradeView.Upgrade);
            _buttonUIFactory.Create(tavernUpgradePointButtons.InventoryButtonUI,
                playerInventoryUpgradeView.Upgrade);
            _buttonUIFactory.Create(tavernUpgradePointButtons.MovementButtonUI,
                playerMovementUpgradeView.Upgrade);

            //TavernMood
            ImageUI imageUI = _hud.GetComponentInChildren<ImageUI>();
            _imageUIFactory.Create(imageUI);
            TavernMoodView tavernMoodView = _hud.GetComponentInChildren<TavernMoodView>();
            _tavernMoodViewFactory.Create(tavernMoodView, _tavern.TavernMood, imageUI);
            
            //TavernPickUpPoints
            //TODO плохо
            BeerPickUpPointView beerPickUpPointView =
                Object.FindObjectOfType<BeerPickUpPointView>();
            PickUpPointUIImages beerPickUpPointImageUI =
                beerPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(beerPickUpPointView, beerPickUpPointImageUI,
                _imageUIFactory, beerConfig);

            //TODO плохо
            BreadPickUpPointView breadPickUpPointView =
                Object.FindObjectOfType<BreadPickUpPointView>();
            PickUpPointUIImages breadPickUpPointImageUI =
                beerPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(breadPickUpPointView, breadPickUpPointImageUI,
                _imageUIFactory, breadConfig);
            
            //TODO плохо
            MeatPickUpPointView meatPickUpPointView =
                Object.FindObjectOfType<MeatPickUpPointView>();
            PickUpPointUIImages meatPickUpPointImageUI =
                meatPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(meatPickUpPointView, meatPickUpPointImageUI,
                _imageUIFactory, meatConfig);
            
            //TODO плохо
            SoupPickUpPointView soupPickUpPointView =
                Object.FindObjectOfType<SoupPickUpPointView>();
            PickUpPointUIImages soupPickUpPointImageUI =
                soupPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(soupPickUpPointView, soupPickUpPointImageUI,
                _imageUIFactory, soupConfig);

            //TODO плохо
            WinePickUpPointView winePickUpPointView =
                Object.FindObjectOfType<WinePickUpPointView>();
            PickUpPointUIImages winePickUpPointImageUI =
                soupPickUpPointView.GetComponentInChildren<PickUpPointUIImages>();
            _tavernFoodPickUpPointViewFactory.Create(winePickUpPointView, winePickUpPointImageUI,
                _imageUIFactory, wineConfig);
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