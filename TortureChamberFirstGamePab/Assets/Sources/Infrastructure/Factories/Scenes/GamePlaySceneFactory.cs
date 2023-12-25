using System;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.Domain.PlayerMovement.PlayerMovementCharacteristics;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Infrastructure.Factorys.Views;
using MyProject.Sources.Presentation.Animations;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerCameras;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.BuilderFactories;
using Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factorys;
using Sources.Infrastructure.Factorys.Controllers;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.Infrastructure.Factorys.Views;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.SceneService;
using Sources.InfrastructureInterfaces.Factorys.Scenes;
using Sources.Presentation.Animations;
using Sources.Presentation.UI;
using Sources.Presentation.Views;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Taverns.Foods;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;
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

            //Items
            ItemConfig beerConfig = Resources.Load<ItemConfig>(BeerItemConfigPath);
            IItem[] items = new IItem[]
            {
                new Beer(beerConfig),
            };
            itemRepository.AddCollection(items);

            //ItemsView
            // BeerView beerView = Resources.Load<BeerView>();
            IItemView[] itemViews = new IItemView[]
            {
            };

            //ItemFactory
            foreach (IItem item in items)
            {
                itemRepository.Add(item);
            }

            ItemsFactory itemsFactory = new ItemsFactory(items);

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
            
            //Visitor
            VisitorBuilder visitorBuilder = new VisitorBuilder(collectionRepository, itemRepository);
            visitorBuilder.Create(imageUIFactory);

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
            TextUI textUI = hud.GetComponentInChildren<TextUI>();
            playerInventoryViewFactory.Create(playerInventoryView, textUI, playerInventory,
            itemViewFactory, imageUIFactory);
            
            //BeerPickUpPoint
            ImageUI imageUI = beerPickUpPointView.gameObject.GetComponentInChildren<ImageUI>();
            TavernPickUpPointPresenterFactory tavernPickUpPointPresenterFactory =
                new TavernPickUpPointPresenterFactory(itemsFactory);
            TavernBeerPickUpPointViewFactory tavernBeerPickUpPointViewFactory =
                new TavernBeerPickUpPointViewFactory(tavernPickUpPointPresenterFactory);
            tavernBeerPickUpPointViewFactory.Create(beerPickUpPointView, imageUI, imageUIFactory);
            
            return new GamePlayScene
            (
                inputService,
                updateService
            );
        }
    }
}