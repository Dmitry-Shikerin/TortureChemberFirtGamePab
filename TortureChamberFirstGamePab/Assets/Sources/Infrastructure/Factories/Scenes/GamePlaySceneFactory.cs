using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Builders;
using Sources.Infrastructure.Factories.Controllers.Items.Coins;
using Sources.Infrastructure.Factories.Controllers.Items.Garbages;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Infrastructure.Factories.Controllers.Points;
using Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Domains.Items;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Repositoryes;
using Sources.Infrastructure.Factories.Views.Items.Coins;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.Items.Garbeges;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.Movement;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Triggers.Taverns;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Items.Coins;
using Sources.Presentation.Views.Items.Garbages;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using Sources.Presentation.Views.UIs;
using Sources.Presentation.Voids;
using Sources.Presentation.Voids.GamePoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GamePlaySceneFactory : ISceneFactory
    {
        private readonly SceneService _sceneService;
        private readonly PlayerDataService _playerDataService;
        private readonly PlayerUpgradeDataService _playerUpgradeDataService;

        private const string PlayerMovementCharacteristicsPath = "Configs/PlayerMovementCharacteristics";
        private const string BeerItemConfigPath = "Configs/Items/BeerItemConfig";
        private const string BreadItemConfigPath = "Configs/Items/BreadItemConfig";
        private const string MeatItemConfigPath = "Configs/Items/MeatItemConfig";
        private const string SoupItemConfigPath = "Configs/Items/SoupItemConfig";
        private const string WineItemConfigPath = "Configs/Items/WineItemConfig";

        public GamePlaySceneFactory(SceneService sceneService, PlayerDataService playerDataService,
            PlayerUpgradeDataService playerUpgradeDataService)
        {
            _sceneService = sceneService ??
                            throw new ArgumentNullException(nameof(sceneService));
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
            _playerUpgradeDataService = playerUpgradeDataService ?? throw new ArgumentNullException(nameof(playerUpgradeDataService));
        }

        public async UniTask<IScene> Create(object payload)
        {
            PlayerCameraView playerCameraView =
                Object.FindObjectOfType<PlayerCameraView>();
            HUD hud = Object.FindObjectOfType<HUD>();
            BeerPickUpPointView beerPickUpPointView =
                Object.FindObjectOfType<BeerPickUpPointView>();
            BreadPickUpPointView breadPickUpPointView =
                Object.FindObjectOfType<BreadPickUpPointView>();
            MeatPickUpPointView meatPickUpPointView =
                Object.FindObjectOfType<MeatPickUpPointView>();
            SoupPickUpPointView soupPickUpPointView =
                Object.FindObjectOfType<SoupPickUpPointView>();
            WinePickUpPointView winePickUpPointView =
                Object.FindObjectOfType<WinePickUpPointView>();
            TavernUpgradePointButtons tavernUpgradePointButtons =
                Object.FindObjectOfType<TavernUpgradePointButtons>(true);
            TavernUpgradeTrigger tavernUpgradeTrigger =
                Object.FindObjectOfType<TavernUpgradeTrigger>();
            TavernUpgradePointView tavernUpgradePointView =
                Object.FindObjectOfType<TavernUpgradePointView>(true);

            HudTextUIContainer hudTextUIContainer = hud.GetComponent<HudTextUIContainer>();

            //RootGamePoints
            RootGamePoints rootGamePoints = Object.FindObjectOfType<RootGamePoints>();

            //VisitorPointRepository
            VisitorPointRepositoryFactory visitorPointRepositoryFactory =
                new VisitorPointRepositoryFactory(rootGamePoints);
            CollectionRepository collectionRepository = visitorPointRepositoryFactory.Create();

            //ItemRepository
            ItemRepository<IItem> itemRepository = new ItemRepository<IItem>();

            //TavernUpgradePointService
            TavernUpgradePointService tavernUpgradePointService = new TavernUpgradePointService(
                tavernUpgradeTrigger, tavernUpgradePointView);

            //EatPointFactories
            EatPointPresenterFactory eatPointPresenterFactory = new EatPointPresenterFactory();
            EatPointViewFactory eatPointViewFactory = new EatPointViewFactory(eatPointPresenterFactory);

            //SeatPoints
            SeatPointPresenterFactory seatPointPresenterFactory = new SeatPointPresenterFactory();
            SeatPointViewFactory seatPointViewFactory = new SeatPointViewFactory(seatPointPresenterFactory);

            List<SeatPointView> seatPoints = new List<SeatPointView>();
            foreach (SeatPointView seatPointView in rootGamePoints.GetComponentsInChildren<SeatPointView>())
            {
                seatPointViewFactory.Create(seatPointView);
                eatPointViewFactory.Create(seatPointView.EatPointView);
                seatPoints.Add(seatPointView);
            }

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
            itemRepository.AddCollection(items);

            //ItemsView
            // BeerView beerView = Resources.Load<BeerView>();
            IItemView[] itemViews = new IItemView[]
            {
            };

            //ItemFactory
            // foreach (IItem item in items)
            // {
            //     itemRepository.Add(item);
            // }

            ItemsFactory itemsFactory = new ItemsFactory(items);

            //ShuffleService
            ProductShuffleService productShuffleService = new ProductShuffleService(items.ToList());

            //UpdateServise
            UpdateService updateService = new UpdateService();

            //InputService
            InputService inputService = new InputService();

            //PrefabFactory
            PrefabFactory prefabFactory = new PrefabFactory();

            //CameraDirectionService
            CameraDirectionService cameraDirectionService = new CameraDirectionService(playerCameraView);

            //ItemViewFactory
            ItemViewFactory itemViewFactory = new ItemViewFactory(prefabFactory);

            //ImageUiFactories
            ImageUIPresenterFactory imageUIPresenterFactory = new ImageUIPresenterFactory();
            ImageUIFactory imageUIFactory = new ImageUIFactory(imageUIPresenterFactory);

            //ButtonUIFactories
            ButtonUIPresenterFactory buttonUIPresenterFactory = new ButtonUIPresenterFactory();
            ButtonUIFactory buttonUIFactory = new ButtonUIFactory(buttonUIPresenterFactory);

            //TextUIFactories
            TextUIPresenterFactory textUIPresenterFactory = new TextUIPresenterFactory();
            TextUIFactory textUIFactory = new TextUIFactory(textUIPresenterFactory);

            //CoinAnimationFactories
            ObjectPool<CoinAnimationView> coinAnimationViewObjectPool = new ObjectPool<CoinAnimationView>();
            CoinAnimationPresenterFactory coinAnimationPresenterFactory = new CoinAnimationPresenterFactory();
            CoinAnimationViewFactory coinAnimationViewFactory =
                new CoinAnimationViewFactory(coinAnimationPresenterFactory,
                    prefabFactory, coinAnimationViewObjectPool);
            CoinSpawner coinSpawner = new CoinSpawner(coinAnimationViewFactory, coinAnimationViewObjectPool);

            // нужна здесь для DI
            PlayerMovementCharacteristic playerMovementCharacteristic =
                Resources.Load<PlayerMovementCharacteristic>(PlayerMovementCharacteristicsPath);


            //PlayerUpgradeContainers
            // UpgradeConfig charismaUpgradeConfig =
            //     Resources.Load<UpgradeConfig>("Configs/Upgrades/CharismaUpgradeConfig");
            // UpgradeConfig inventoryUpgradeConfig =
            //     Resources.Load<UpgradeConfig>("Configs/Upgrades/InventoryUpgradeConfig");
            // UpgradeConfig movementUpgradeConfig =
            //     Resources.Load<UpgradeConfig>("Configs/Upgrades/MovementUpgradeConfig");
            //
            // Upgrader playerCharismaUpgrader = new Upgrader(charismaUpgradeConfig);
            // Upgrader playerInventoryUpgrader = new Upgrader(inventoryUpgradeConfig);
            // Upgrader playerMovementUpgrader = new Upgrader(movementUpgradeConfig);

            //TavernMood
            // TavernMood tavernMood = new TavernMood();
            // ImageUI imageUI = hud.GetComponentInChildren<ImageUI>();
            // imageUIFactory.Create(imageUI);
            // TavernMoodView tavernMoodView = hud.GetComponentInChildren<TavernMoodView>();
            // TavernMoodPresenterFactory tavernMoodPresenterFactory =
            //     new TavernMoodPresenterFactory(playerCharismaUpgrader);
            // TavernMoodViewFactory tavernMoodViewFactory = new TavernMoodViewFactory(tavernMoodPresenterFactory);
            // tavernMoodViewFactory.Create(tavernMoodView, tavernMood, imageUI);

            //TODO придумать обобщение для текстUI
            //GamePlayService
            // GamePlay gamePlay = new GamePlay();
            // GamePlayService gamePlayService = new GamePlayService(gamePlay, seatPoints.Count);

            //TODO подумать над ISP над принципом разделения интерфейсов
            //TODO разделить интерфейсы на вьюшках
            //TODO подумать над тем что должно быть во вьшке а что нет
            
            //GarbadgeView
            ObjectPool<GarbageView> garbageViewObjectPool = new ObjectPool<GarbageView>();
            GarbagePresenterFactory garbagePresenterFactory = new GarbagePresenterFactory();
            GarbageViewFactory garbageViewFactory = new GarbageViewFactory(
                garbagePresenterFactory, garbageViewObjectPool, prefabFactory, imageUIFactory);

            //GarbageBuilder
            GarbageSpawner garbageSpawner = new GarbageSpawner(
                garbageViewFactory, garbageViewObjectPool);

            // //VisitorCounter
            // VisitorCounter visitorCounter = new VisitorCounter();

            // //Visitor
            // ObjectPool<VisitorView> visitorViewObjectPool = new ObjectPool<VisitorView>();
            //
            // VisitorPresenterFactory visitorPresenterFactory = new VisitorPresenterFactory(
            //     collectionRepository, productShuffleService, imageUIFactory, itemViewFactory, garbageSpawner,
            //     coinSpawner);
            // VisitorViewFactory visitorViewFactory = new VisitorViewFactory(visitorPresenterFactory,
            //     prefabFactory, visitorViewObjectPool);

            //VisitorSpawnService
            // VisitorSpawnService visitorSpawnService = new VisitorSpawnService(
            //     gamePlay, visitorCounter, prefabFactory, visitorViewObjectPool,
            //     visitorViewFactory, tavernMood);

            //PLayerWallet
            PlayerWalletPresenterFactory playerWalletPresenterFactory = new PlayerWalletPresenterFactory();

            PlayerWalletViewFactory playerWalletViewFactory =
                new PlayerWalletViewFactory(playerWalletPresenterFactory);
            PlayerCameraPresenterFactory playerCameraPresenterFactory =
                new PlayerCameraPresenterFactory(inputService, updateService);

            PlayerCameraViewFactory playerCameraViewFactory =
                new PlayerCameraViewFactory(playerCameraPresenterFactory);

            // PlayerInventoryPresenterFactory playerInventoryPresenterFactory =
            //     new PlayerInventoryPresenterFactory(playerInventoryUpgrader,
            //         itemViewFactory, hudTextUIContainer.SystemErrorsText);

            // PlayerInventoryViewFactory playerInventoryViewFactory =
            //     new PlayerInventoryViewFactory(playerInventoryPresenterFactory,
            //         imageUIFactory);

            textUIFactory.Create(hudTextUIContainer.SystemErrorsText,
                new ObservableProperty<string>());

            //PlayerMovementFactorys
            // PlayerMovementService playerMovementService =
            //     new PlayerMovementService(playerMovementUpgrader, playerMovementCharacteristic);

            // PlayerMovementPresenterFactory playerMovementPresenterFactory =
            //     new PlayerMovementPresenterFactory(inputService, updateService,
            //         cameraDirectionService, playerMovementService);

            // PlayerMovementViewFactory playerMovementViewFactory =
            //     new PlayerMovementViewFactory(playerMovementPresenterFactory);

            //PickUpPointsFactories
            TavernPickUpPointPresenterFactory tavernPickUpPointPresenterFactory =
                new TavernPickUpPointPresenterFactory(itemsFactory);

            TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory =
                new TavernFoodPickUpPointViewFactory(tavernPickUpPointPresenterFactory);

            //BeerPickUpPoint
            PickUpPointUIImages berPickUpPointUIImages =
                beerPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(beerPickUpPointView, berPickUpPointUIImages, imageUIFactory,
                beerConfig);

            //BreadPickUpPoint
            PickUpPointUIImages breadPickUpPointUIImages =
                breadPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(breadPickUpPointView, breadPickUpPointUIImages, imageUIFactory,
                breadConfig);

            //MeatPickUpPoint
            PickUpPointUIImages meatPickUpPointUIImages =
                meatPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(meatPickUpPointView, meatPickUpPointUIImages, imageUIFactory,
                meatConfig);

            //SoupPickUpPoint
            PickUpPointUIImages soupPickUpPointUIImages =
                soupPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(soupPickUpPointView, soupPickUpPointUIImages, imageUIFactory,
                soupConfig);

            //WinePickUpPoint
            PickUpPointUIImages winePickUpPointUIImages =
                winePickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(winePickUpPointView, winePickUpPointUIImages, imageUIFactory,
                wineConfig);

            //PauseMenuService
            PauseMenuWindow pauseMenuWindow = hud.GetComponentInChildren<PauseMenuWindow>(true);
            PauseMenuService pauseMenuService = new PauseMenuService(inputService, pauseMenuWindow);

            bool canLoad = payload is LoadServicePayload { CanLoad: true };

            // ILoadService loadService = CreateLoadService
            // (
            //     playerMovementViewFactory,
            //     playerCameraViewFactory,
            //     playerInventoryViewFactory,
            //     playerWalletViewFactory,
            //     textUIFactory,
            //     buttonUIFactory,
            //     canLoad
            // );
        
        
            return new GamePlayScene
            (
                inputService,
                updateService,
                // visitorSpawnService,
                tavernUpgradePointService
                // gamePlayService,
                // loadService
            );
        }

        // private ILoadService CreateLoadService
        // (
        //     PlayerMovementViewFactory playerMovementViewFactory,
        //     PlayerCameraViewFactory playerCameraViewFactory,
        //     PlayerInventoryViewFactory playerInventoryViewFactory,
        //     PlayerWalletViewFactory playerWalletViewFactory,
        //     TextUIFactory textUIFactory,
        //     ButtonUIFactory buttonUIFactory,
        //     bool canLoad)
        // {
        //     if (canLoad == false)
        //     {
        //         return new CreateService
        //         (
        //             playerMovementViewFactory,
        //             playerCameraViewFactory,
        //             playerInventoryViewFactory,
        //             playerWalletViewFactory,
        //             textUIFactory,
        //             buttonUIFactory,
        //             _playerDataService,
        //             _playerUpgradeDataService
        //         );
        //     }
        //
        //     return new LoadService
        //     (
        //         playerMovementViewFactory,
        //         playerCameraViewFactory,
        //         playerInventoryViewFactory,
        //         playerWalletViewFactory,
        //         _playerDataService,
        //         _playerUpgradeDataService,
        //         textUIFactory,
        //         buttonUIFactory
        //     );
        // }
    }
}