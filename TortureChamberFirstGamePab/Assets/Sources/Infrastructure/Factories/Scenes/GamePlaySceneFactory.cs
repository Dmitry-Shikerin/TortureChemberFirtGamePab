using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Domains.Items;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.Presentation.Triggers.Taverns;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using Sources.Presentation.Voids;
using Sources.Presentation.Voids.GamePoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GamePlaySceneFactory : ISceneFactory
    {
        private readonly SceneService _sceneService;
        private readonly PlayerDataService _playerDataService;
        private readonly PlayerUpgradeDataService _playerUpgradeDataService;
        private SceneContext _sceneContext;

        private const string PlayerMovementCharacteristicsPath = "Configs/PlayerMovementCharacteristics";

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

            _sceneContext = Object.FindObjectOfType<SceneContext>();

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
            
            //ItemsView
            // BeerView beerView = Resources.Load<BeerView>();
            IItemView[] itemViews = new IItemView[]
            {
            };

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
            // ObjectPool<GarbageView> garbageViewObjectPool = new ObjectPool<GarbageView>();
            // GarbagePresenterFactory garbagePresenterFactory = new GarbagePresenterFactory();
            // GarbageViewFactory garbageViewFactory = new GarbageViewFactory(
            //     garbagePresenterFactory, garbageViewObjectPool, prefabFactory, imageUIFactory);

            //GarbageBuilder
            // GarbageSpawner garbageSpawner = new GarbageSpawner(
            //     garbageViewFactory, garbageViewObjectPool);
            

            //PLayerWallet
            // PlayerWalletPresenterFactory playerWalletPresenterFactory = new PlayerWalletPresenterFactory();
            //
            // PlayerWalletViewFactory playerWalletViewFactory =
            //     new PlayerWalletViewFactory(playerWalletPresenterFactory);
            
            // PlayerCameraPresenterFactory playerCameraPresenterFactory =
            //     new PlayerCameraPresenterFactory(inputService, updateService);
            //
            // PlayerCameraViewFactory playerCameraViewFactory =
            //     new PlayerCameraViewFactory(playerCameraPresenterFactory);

            // PlayerInventoryPresenterFactory playerInventoryPresenterFactory =
            //     new PlayerInventoryPresenterFactory(playerInventoryUpgrader,
            //         itemViewFactory, hudTextUIContainer.SystemErrorsText);

            // PlayerInventoryViewFactory playerInventoryViewFactory =
            //     new PlayerInventoryViewFactory(playerInventoryPresenterFactory,
            //         imageUIFactory);

            // textUIFactory.Create(hudTextUIContainer.SystemErrorsText,
            //     new ObservableProperty<string>());

            //PlayerMovementFactorys
            // PlayerMovementService playerMovementService =
            //     new PlayerMovementService(playerMovementUpgrader, playerMovementCharacteristic);

            // PlayerMovementPresenterFactory playerMovementPresenterFactory =
            //     new PlayerMovementPresenterFactory(inputService, updateService,
            //         cameraDirectionService, playerMovementService);

            // PlayerMovementViewFactory playerMovementViewFactory =
            //     new PlayerMovementViewFactory(playerMovementPresenterFactory);

            //PickUpPointsFactories
            // TavernPickUpPointPresenterFactory tavernPickUpPointPresenterFactory =
            //     new TavernPickUpPointPresenterFactory(itemsFactory);
            //
            // TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory =
            //     new TavernFoodPickUpPointViewFactory(tavernPickUpPointPresenterFactory);

            //BeerPickUpPoint
            // PickUpPointUIImages berPickUpPointUIImages =
            //     beerPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            // tavernFoodPickUpPointViewFactory.Create(beerPickUpPointView, berPickUpPointUIImages, imageUIFactory,
            //     beerConfig);
            //
            // //BreadPickUpPoint
            // PickUpPointUIImages breadPickUpPointUIImages =
            //     breadPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            // tavernFoodPickUpPointViewFactory.Create(breadPickUpPointView, breadPickUpPointUIImages, imageUIFactory,
            //     breadConfig);
            //
            // //MeatPickUpPoint
            // PickUpPointUIImages meatPickUpPointUIImages =
            //     meatPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            // tavernFoodPickUpPointViewFactory.Create(meatPickUpPointView, meatPickUpPointUIImages, imageUIFactory,
            //     meatConfig);
            //
            // //SoupPickUpPoint
            // PickUpPointUIImages soupPickUpPointUIImages =
            //     soupPickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            // tavernFoodPickUpPointViewFactory.Create(soupPickUpPointView, soupPickUpPointUIImages, imageUIFactory,
            //     soupConfig);
            //
            // //WinePickUpPoint
            // PickUpPointUIImages winePickUpPointUIImages =
            //     winePickUpPointView.gameObject.GetComponentInChildren<PickUpPointUIImages>();
            // tavernFoodPickUpPointViewFactory.Create(winePickUpPointView, winePickUpPointUIImages, imageUIFactory,
            //     wineConfig);

            //PauseMenuService
            // PauseMenuWindow pauseMenuWindow = hud.GetComponentInChildren<PauseMenuWindow>(true);
            // PauseMenuService pauseMenuService = new PauseMenuService(inputService, pauseMenuWindow);

            bool canLoad = payload is LoadServicePayload { CanLoad: true };

            ILoadService loadService = CreateLoadService(canLoad);
        
        
            return new GamePlayScene
            (
                // Resolve<InputService>(),
                Resolve<IInputService>(),
                Resolve<UpdateService>(),
                // visitorSpawnService,
                Resolve<TavernUpgradePointService>(),
                // gamePlayService,
                loadService
            );
        }

        private ILoadService CreateLoadService(bool canLoad)
        {
            if (canLoad == false)
                return Resolve<CreateService>();

            return Resolve<LoadService>();
        }

        //TODO можно ли так?
        private T Resolve<T>() => 
            _sceneContext.Container.Resolve<T>();
    }
}