using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Controllers.UI;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.Domain.PlayerMovement.PlayerMovementCharacteristics;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Infrastructure.Factorys.Views;
using MyProject.Sources.Presentation.Animations;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces;
using Sources.Domain.GamePlays;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerCameras;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.BuilderFactories;
using Sources.Infrastructure.Factories.Controllers.Items.Coins;
using Sources.Infrastructure.Factories.Controllers.Items.Garbages;
using Sources.Infrastructure.Factories.Controllers.Points;
using Sources.Infrastructure.Factories.Controllers.Taverns;
using Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Items.Coins;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.Items.Garbeges;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factorys;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Infrastructure.Services.SceneService;
using Sources.InfrastructureInterfaces.Factorys.Scenes;
using Sources.Presentation.UI;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.Presentation.Views.Items.Coins;
using Sources.Presentation.Views.Items.Garbages;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Taverns;
using Sources.Presentation.Views.Taverns.Foods;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.Repositoryes;
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
            
            HudTextUIContainer hudTextUIContainer = hud.GetComponent<HudTextUIContainer>();
            
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
            // CoinAnimationView coinAnimationView = Object.FindObjectOfType<CoinAnimationView>();
            CoinAnimationPresenterFactory coinAnimationPresenterFactory = new CoinAnimationPresenterFactory();
            CoinAnimationViewFactory coinAnimationViewFactory = 
                new CoinAnimationViewFactory(coinAnimationPresenterFactory);
            CoinBuilder coinBuilder = new CoinBuilder(prefabFactory, coinAnimationViewFactory);
            
            //TavernMood
            TavernMood tavernMood = new TavernMood();
            ImageUI imageUI = hud.GetComponentInChildren<ImageUI>();
            imageUIFactory.Create(imageUI);
            TavernMoodView tavernMoodView = hud.GetComponentInChildren<TavernMoodView>();
            TavernMoodPresenterFactory tavernMoodPresenterFactory = new TavernMoodPresenterFactory();
            TavernMoodViewFactory tavernMoodViewFactory = new TavernMoodViewFactory(tavernMoodPresenterFactory);
            tavernMoodViewFactory.Create(tavernMoodView, tavernMood, imageUI);
            
            //GamePlayService
            GamePlay gamePlay = new GamePlay();
            GamePlayService gamePlayService = new GamePlayService(gamePlay, seatPoints.Count);
            gamePlayService.Start();
            
            //TODO покашто так проверить
            //GarbadgeView
            // GarbageView garbageView = Object.FindObjectOfType<GarbageView>();
            GarbagePresenterFactory garbagePresenterFactory = new GarbagePresenterFactory();
            GarbageViewFactory garbageViewFactory = new GarbageViewFactory(garbagePresenterFactory);
            // garbageViewFactory.Create(garbageView, imageUIFactory);
            
            //GarbageBuilder
            GarbageBuilder garbageBuilder = new GarbageBuilder(prefabFactory, garbageViewFactory, imageUIFactory);
            
            //Visitor
            //TODO потом удалить этот пул
            ObjectPool<VisitorView> objectPool = new ObjectPool<VisitorView>();
            VisitorBuilder visitorBuilder = new VisitorBuilder(collectionRepository, itemRepository,
                productShuffleService, itemViewFactory, imageUIFactory, tavernMood, garbageBuilder,
                coinBuilder);
            visitorBuilder.Create(objectPool);
            
            //VisitorSpawnService
            // VisitorSpawnService visitorSpawnService = new VisitorSpawnService(updateService ,gamePlay ,visitorBuilder);

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
            playerCameraViewFactory.Create(playerCameraView, playerCamera);

            //PlayerMovement
            playerCameraView.SetTargetTransform(playerMovementView);
            PlayerAnimation playerAnimation =
                playerMovementView.GetComponent<PlayerAnimation>() ??
                throw new NullReferenceException(nameof(PlayerAnimation));
            PlayerMovementCharacteristic playerMovementCharacteristic =
                Resources.Load<PlayerMovementCharacteristic>(PlayerMovementCharacteristicsPath);
            PlayerMovement playerMovement = new PlayerMovement(
                playerMovementCharacteristic);
            PlayerMovementPresenterFactory playerMovementPresenterFactory =
                new PlayerMovementPresenterFactory(inputService, updateService,
                    cameraDirectionService);
            PlayerMovementViewFactory playerMovementViewFactory =
                new PlayerMovementViewFactory(playerMovementPresenterFactory);
            playerMovementViewFactory.Create(playerMovement, playerMovementView, playerAnimation);
            
            //PlayerInventory
            PlayerInventory playerInventory = new PlayerInventory();
            PlayerInventoryPresenterFactory playerInventoryPresenterFactory =
                new PlayerInventoryPresenterFactory();
            PlayerInventoryViewFactory playerInventoryViewFactory =
                new PlayerInventoryViewFactory(playerInventoryPresenterFactory);

            //TODO как исправить заглушку в виде проперти?
            textUIFactory.Create(hudTextUIContainer.SystemErrorsText,
                new ObservableProperty<string>());
            
            playerInventoryViewFactory.Create(playerInventoryView, 
                hudTextUIContainer.SystemErrorsText, playerInventory,
            itemViewFactory, imageUIFactory);
            
            //TavernUpgradePointButtons
            buttonUIFactory.Create(tavernUpgradePointButtons.CharismaButtonUI, tavernMood.AddAmountMood);
            buttonUIFactory.Create(tavernUpgradePointButtons.InventoryButtonUI, playerInventory.AddInventoryCapacity);
            buttonUIFactory.Create(tavernUpgradePointButtons.MovementButtonUI, playerMovement.AddMovementSpeed);

            
            //PickUpPointsFactories
            TavernPickUpPointPresenterFactory tavernPickUpPointPresenterFactory =
                new TavernPickUpPointPresenterFactory(itemsFactory);
            TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory =
                new TavernFoodPickUpPointViewFactory(tavernPickUpPointPresenterFactory);
            
            //BeerPickUpPoint
            PickUpPointUI berPickUpPointUI = beerPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUI>();
            tavernFoodPickUpPointViewFactory.Create(beerPickUpPointView, berPickUpPointUI, imageUIFactory,
                beerConfig);
            
            //BreadPickUpPoint
            PickUpPointUI breadPickUpPointUI = breadPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUI>();
            tavernFoodPickUpPointViewFactory.Create(breadPickUpPointView, breadPickUpPointUI, imageUIFactory,
                breadConfig);
            
            //MeatPickUpPoint
            PickUpPointUI meatPickUpPointUi = meatPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUI>();
            tavernFoodPickUpPointViewFactory.Create(meatPickUpPointView, meatPickUpPointUi, imageUIFactory,
                meatConfig);
            
            //SoupPickUpPoint
            PickUpPointUI soupPickUpPointUI = soupPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUI>();
            tavernFoodPickUpPointViewFactory.Create(soupPickUpPointView, soupPickUpPointUI, imageUIFactory,
                soupConfig);
            
            //WinePickUpPoint
            PickUpPointUI winePickUpPointUI = winePickUpPointView.gameObject.GetComponentInChildren<PickUpPointUI>();
            tavernFoodPickUpPointViewFactory.Create(winePickUpPointView, winePickUpPointUI, imageUIFactory,
                wineConfig);

            
            return new GamePlayScene
            (
                inputService,
                updateService
            );
        }
    }
}