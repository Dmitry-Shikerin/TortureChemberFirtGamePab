using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.GamePlays;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerCameras;
using Sources.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Sources.Domain.Taverns;
using Sources.Domain.Upgrades;
using Sources.Domain.Upgrades.Configs;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Builders;
using Sources.Infrastructure.Collections;
using Sources.Infrastructure.DataSources;
using Sources.Infrastructure.Factories.Controllers.Items.Coins;
using Sources.Infrastructure.Factories.Controllers.Items.Garbages;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Infrastructure.Factories.Controllers.Points;
using Sources.Infrastructure.Factories.Controllers.Taverns;
using Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Controllers.Visitors;
using Sources.Infrastructure.Factories.Domains.Items;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Repositoryes;
using Sources.Infrastructure.Factories.Views.Items.Coins;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.Items.Garbeges;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Movement;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.Stores;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Triggers.Taverns;
using Sources.Presentation.UI;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Taverns;
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

        private const string PlayerMovementCharacteristicsPath = "Configs/PlayerMovementCharacteristics";
        private const string BeerItemConfigPath = "Configs/Items/BeerItemConfig";
        private const string BreadItemConfigPath = "Configs/Items/BreadItemConfig";
        private const string MeatItemConfigPath = "Configs/Items/MeatItemConfig";
        private const string SoupItemConfigPath = "Configs/Items/SoupItemConfig";
        private const string WineItemConfigPath = "Configs/Items/WineItemConfig";

        public GamePlaySceneFactory(SceneService sceneService)
        {
            _sceneService = sceneService ??
                            throw new ArgumentNullException(nameof(sceneService));
        }

        public async UniTask<IScene> Create(object payload)
        {
            PlayerMovementView playerMovementView =
                Object.FindObjectOfType<PlayerMovementView>();
            PlayerCameraView playerCameraView =
                Object.FindObjectOfType<PlayerCameraView>();
            PlayerInventoryView playerInventoryView =
                Object.FindObjectOfType<PlayerInventoryView>();
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

            //TODO сделать абстрактную фабрику
            
            //RootGamePoints
            RootGamePoints rootGamePoints = Object.FindObjectOfType<RootGamePoints>();

            //VisitorPointRepository
            VisitorPointRepositoryFactory visitorPointRepositoryFactory =
                new VisitorPointRepositoryFactory(rootGamePoints);
            CollectionRepository collectionRepository = visitorPointRepositoryFactory.Create();

            //ItemRepository
            ItemRepository<IItem> itemRepository = new ItemRepository<IItem>();

            //ItemViewrepository
            // ItemRepository<IItemView> itemViewRepository = new ItemRepository<IItemView>();

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
            CoinAnimationPresenterFactory coinAnimationPresenterFactory = new CoinAnimationPresenterFactory();
            CoinAnimationViewFactory coinAnimationViewFactory =
                new CoinAnimationViewFactory(coinAnimationPresenterFactory);
            CoinBuilder coinBuilder = new CoinBuilder(prefabFactory, coinAnimationViewFactory);

            // нужна здесь для DI
            PlayerMovementCharacteristic playerMovementCharacteristic =
                Resources.Load<PlayerMovementCharacteristic>(PlayerMovementCharacteristicsPath);

            
            //PlayerUpgradeContainers
            UpgradeConfig charismaUpgradeConfig = 
                Resources.Load<UpgradeConfig>("Configs/Upgrades/CharismaUpgradeConfig");
            UpgradeConfig inventoryUpgradeConfig = 
                Resources.Load<UpgradeConfig>("Configs/Upgrades/InventoryUpgradeConfig");
            UpgradeConfig movementUpgradeConfig = 
                Resources.Load<UpgradeConfig>("Configs/Upgrades/MovementUpgradeConfig");
            
            Upgrader playerCharismaUpgrader = new Upgrader(charismaUpgradeConfig);
            Upgrader playerInventoryUpgrader = new Upgrader(inventoryUpgradeConfig);
            Upgrader playerMovementUpgrader = new Upgrader(movementUpgradeConfig);
            
            //TavernMood
            TavernMood tavernMood = new TavernMood();
            ImageUI imageUI = hud.GetComponentInChildren<ImageUI>();
            imageUIFactory.Create(imageUI);
            TavernMoodView tavernMoodView = hud.GetComponentInChildren<TavernMoodView>();
            TavernMoodPresenterFactory tavernMoodPresenterFactory = 
                new TavernMoodPresenterFactory(playerCharismaUpgrader);
            TavernMoodViewFactory tavernMoodViewFactory = new TavernMoodViewFactory(tavernMoodPresenterFactory);
            tavernMoodViewFactory.Create(tavernMoodView, tavernMood, imageUI);

            //GamePlayService
            GamePlay gamePlay = new GamePlay();
            GamePlayService gamePlayService = new GamePlayService(gamePlay, seatPoints.Count);

            //GarbadgeView
            GarbagePresenterFactory garbagePresenterFactory = new GarbagePresenterFactory();
            GarbageViewFactory garbageViewFactory = new GarbageViewFactory(garbagePresenterFactory);

            //GarbageBuilder
            GarbageBuilder garbageBuilder = new GarbageBuilder(prefabFactory, garbageViewFactory, imageUIFactory);

            //VisitorCounter
            VisitorCounter visitorCounter = new VisitorCounter();

            //Visitor
            VisitorPresenterFactory visitorPresenterFactory = new VisitorPresenterFactory(
                collectionRepository, productShuffleService, imageUIFactory, itemViewFactory, garbageBuilder,
                coinBuilder);
            VisitorBuilder visitorBuilder = new VisitorBuilder(
                visitorPresenterFactory, tavernMood, visitorCounter);
            // visitorBuilder.Create(objectPool);

            //VisitorSpawnService
            VisitorSpawnService visitorSpawnService = new VisitorSpawnService(
                gamePlay, visitorBuilder, visitorCounter, prefabFactory);

            //PLayerWallet
            PlayerWalletView playerWalletView = playerMovementView.GetComponent<PlayerWalletView>();
            PlayerWallet playerWallet = new PlayerWallet();
            PlayerWalletPresenterFactory playerWalletPresenterFactory = new PlayerWalletPresenterFactory();

            PlayerWalletViewFactory playerWalletViewFactory =
                new PlayerWalletViewFactory(playerWalletPresenterFactory);

            playerWalletViewFactory.Create(playerWalletView, playerWallet);


            textUIFactory.Create(hudTextUIContainer.PlayerWalletText, playerWallet.Coins);

            //PlayerCamera
            PlayerCamera playerCamera = new PlayerCamera();
            playerCamera.SetStartAngleY(playerCameraView.transform.position.y);

            PlayerCameraPresenterFactory playerCameraPresenterFactory =
                new PlayerCameraPresenterFactory(inputService, updateService);

            PlayerCameraViewFactory playerCameraViewFactory =
                new PlayerCameraViewFactory(playerCameraPresenterFactory);

            // playerCameraViewFactory.Create(playerCameraView, playerCamera);

            //PlayerInventory
            PlayerInventory playerInventory = new PlayerInventory();

            PlayerInventoryPresenterFactory playerInventoryPresenterFactory =
                new PlayerInventoryPresenterFactory(playerInventoryUpgrader,
                    itemViewFactory, hudTextUIContainer.SystemErrorsText);

            PlayerInventoryViewFactory playerInventoryViewFactory =
                new PlayerInventoryViewFactory(playerInventoryPresenterFactory);

            //TODO как исправить заглушку в виде проперти?
            //TODO если через интефей вьюшку можно брать дополнительную вьюку
            //TODO нужен посредник для вьюки чтобы добавить его в презентер
            //TODO сделать еще один презентер здя текс юай
            textUIFactory.Create(hudTextUIContainer.SystemErrorsText,
                new ObservableProperty<string>());

            playerInventoryViewFactory.Create(playerInventoryView, playerInventory, imageUIFactory);
            
            //TODO потом подправить
            playerInventoryView.PlayerInventorySlots[1].BackgroundImage.HideImage();
            playerInventoryView.PlayerInventorySlots[1].Image.HideImage();
            playerInventoryView.PlayerInventorySlots[2].BackgroundImage.HideImage();
            playerInventoryView.PlayerInventorySlots[2].Image.HideImage();
            
            //PlayerMovement
            // playerCameraView.SetTargetTransform(playerMovementView);

            // PlayerAnimation playerAnimation =
            //     playerMovementView.GetComponent<PlayerAnimation>() ??
            //     throw new NullReferenceException(nameof(PlayerAnimation));
            //
            //
            // PlayerMovement playerMovement = new PlayerMovement(
            //     playerMovementCharacteristic, playerMovementUpgrader);

            
            PlayerMovementService playerMovementService = 
                new PlayerMovementService(playerMovementUpgrader,playerMovementCharacteristic);
            
            PlayerMovementPresenterFactory playerMovementPresenterFactory =
                new PlayerMovementPresenterFactory(inputService, updateService,
                    cameraDirectionService, playerMovementService);

            PlayerMovementViewFactory playerMovementViewFactory =
                new PlayerMovementViewFactory(playerMovementPresenterFactory);

            // playerMovementViewFactory.Create(playerMovement);

            //TODO могу ли я сделать абстрактную фабрику для всех вьюшек?

            //UpgradeServices
            //TODO потом сделать по человечески
            TavernUpgradePointTextUIs tavernUpgradePointTextUIs =
                tavernUpgradePointButtons.GetComponent<TavernUpgradePointTextUIs>();

            //TODO не забыть построить все презенторы для текстов
            textUIFactory.Create(tavernUpgradePointTextUIs.PlayerCharismaLevelUpgradeTextUI,
                new ObservableProperty<int>());
            textUIFactory.Create(tavernUpgradePointTextUIs.PlayerCharismaPriceNextLevelUpgradeTextUI,
                new ObservableProperty<int>());
            PlayerUpgradeService playerCharismaUpgradeService = new PlayerUpgradeService(
                playerCharismaUpgrader, tavernUpgradePointTextUIs.PlayerCharismaLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerCharismaPriceNextLevelUpgradeTextUI,
                playerWallet);

            textUIFactory.Create(tavernUpgradePointTextUIs.PlayerMovementSpeedLevelUpgradeTextUI,
                new ObservableProperty<int>());
            textUIFactory.Create(tavernUpgradePointTextUIs.PlayerMovementPriceNextSpeedLevelUpgradeTextUI,
                new ObservableProperty<int>());
            PlayerUpgradeService playerMovementUpgradeService = new PlayerUpgradeService(
                playerMovementUpgrader, tavernUpgradePointTextUIs.PlayerMovementSpeedLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerMovementPriceNextSpeedLevelUpgradeTextUI,
                playerWallet);

            textUIFactory.Create(tavernUpgradePointTextUIs.PlayerInventoryLevelUpgradeTextUI,
                new ObservableProperty<int>());
            textUIFactory.Create(tavernUpgradePointTextUIs.PlayerInventoryPriceNextLevelUpgradeTextUI,
                new ObservableProperty<int>());
            PlayerUpgradeService playerInventoryUpgradeService = new PlayerUpgradeService(
                playerInventoryUpgrader, tavernUpgradePointTextUIs.PlayerInventoryLevelUpgradeTextUI,
                tavernUpgradePointTextUIs.PlayerInventoryPriceNextLevelUpgradeTextUI,
                playerWallet);

            PlayerUpgradeService[] playerUpgradeServices = new PlayerUpgradeService[]
            {
                playerCharismaUpgradeService,
                playerMovementUpgradeService,
                playerInventoryUpgradeService
            };

            //TavernUpgradePointButtons
            buttonUIFactory.Create(tavernUpgradePointButtons.CharismaButtonUI,
                playerCharismaUpgradeService.Upgrade);

            buttonUIFactory.Create(tavernUpgradePointButtons.InventoryButtonUI,
                playerInventoryUpgradeService.Upgrade);

            buttonUIFactory.Create(tavernUpgradePointButtons.MovementButtonUI,
                playerMovementUpgradeService.Upgrade);

            //PickUpPointsFactories
            TavernPickUpPointPresenterFactory tavernPickUpPointPresenterFactory =
                new TavernPickUpPointPresenterFactory(itemsFactory);

            TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory =
                new TavernFoodPickUpPointViewFactory(tavernPickUpPointPresenterFactory);

            //BeerPickUpPoint
            PickUpPointUIImages berPickUpPointUIImages = beerPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(beerPickUpPointView, berPickUpPointUIImages, imageUIFactory,
                beerConfig);

            //BreadPickUpPoint
            PickUpPointUIImages breadPickUpPointUIImages = breadPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(breadPickUpPointView, breadPickUpPointUIImages, imageUIFactory,
                breadConfig);

            //MeatPickUpPoint
            PickUpPointUIImages meatPickUpPointUIImages = meatPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(meatPickUpPointView, meatPickUpPointUIImages, imageUIFactory,
                meatConfig);

            //SoupPickUpPoint
            PickUpPointUIImages soupPickUpPointUIImages = soupPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(soupPickUpPointView, soupPickUpPointUIImages, imageUIFactory,
                soupConfig);

            //WinePickUpPoint
            PickUpPointUIImages winePickUpPointUIImages = winePickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            tavernFoodPickUpPointViewFactory.Create(winePickUpPointView, winePickUpPointUIImages, imageUIFactory,
                wineConfig);
            
            //PauseMenuService
            PauseMenuWindow pauseMenuWindow = hud.GetComponentInChildren<PauseMenuWindow>(true);
            PauseMenuService pauseMenuService = new PauseMenuService(inputService, pauseMenuWindow);

            //ViewFactoryCollection
            ViewFactoryCollection viewFactoryCollection = new ViewFactoryCollection();
            viewFactoryCollection.Register(playerMovementViewFactory);
            viewFactoryCollection.Register(playerCameraViewFactory);
            viewFactoryCollection.Register(playerInventoryViewFactory);
            
            //StorableRepositoryes
            StorableRepository storableRepository = new StorableRepository();
            
            //DataSources
            PlayerPrefsDataSource playerPrefsDataSource = new PlayerPrefsDataSource();
            
            //StoreService
            StoreService storeService = new StoreService(
                storableRepository, playerPrefsDataSource, viewFactoryCollection);
            
            
            return new GamePlayScene
            (
                inputService,
                updateService,
                visitorSpawnService,
                tavernUpgradePointService,
                gamePlayService,
                playerUpgradeServices,
                storeService,
                storableRepository,
                playerMovementViewFactory,
                playerCameraViewFactory,
                pauseMenuService
            );
        }
    }
}