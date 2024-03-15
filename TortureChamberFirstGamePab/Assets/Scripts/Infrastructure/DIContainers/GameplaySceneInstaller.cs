using Lean.Localization;
using Scripts.Domain.Constants;
using Scripts.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Scripts.DomainInterfaces.Items;
using Scripts.Infrastructure.Factories.Controllers.Forms;
using Scripts.Infrastructure.Factories.Controllers.Forms.Gameplays;
using Scripts.Infrastructure.Factories.Controllers.Items.Coins;
using Scripts.Infrastructure.Factories.Controllers.Items.Garbages;
using Scripts.Infrastructure.Factories.Controllers.Players;
using Scripts.Infrastructure.Factories.Controllers.Points;
using Scripts.Infrastructure.Factories.Controllers.Taverns;
using Scripts.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Scripts.Infrastructure.Factories.Controllers.UI;
using Scripts.Infrastructure.Factories.Controllers.UI.AudioSources;
using Scripts.Infrastructure.Factories.Controllers.Visitors;
using Scripts.Infrastructure.Factories.Domains.Items;
using Scripts.Infrastructure.Factories.Repositoryes;
using Scripts.Infrastructure.Factories.Scenes;
using Scripts.Infrastructure.Factories.Services.Forms;
using Scripts.Infrastructure.Factories.Views.Items.Coins;
using Scripts.Infrastructure.Factories.Views.Items.Common;
using Scripts.Infrastructure.Factories.Views.Items.Garbeges;
using Scripts.Infrastructure.Factories.Views.Players;
using Scripts.Infrastructure.Factories.Views.Points;
using Scripts.Infrastructure.Factories.Views.Taverns;
using Scripts.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Scripts.Infrastructure.Factories.Views.UI;
using Scripts.Infrastructure.Factories.Views.UI.AudioSources;
using Scripts.Infrastructure.Factories.Views.Visitors;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.Cameras;
using Scripts.Infrastructure.Services.Forms;
using Scripts.Infrastructure.Services.LoadServices;
using Scripts.Infrastructure.Services.Movement;
using Scripts.Infrastructure.Services.ObjectPools;
using Scripts.Infrastructure.Services.Providers.Players;
using Scripts.Infrastructure.Services.Providers.Taverns;
using Scripts.Infrastructure.Services.Providers.Upgrades;
using Scripts.Infrastructure.Services.UpgradeServices;
using Scripts.Infrastructure.Services.YandexSDCServices;
using Scripts.Infrastructure.Spawners.Generic;
using Scripts.InfrastructureInterfaces.Services;
using Scripts.InfrastructureInterfaces.Services.Cameras;
using Scripts.InfrastructureInterfaces.Services.InputServices;
using Scripts.InfrastructureInterfaces.Services.Movement;
using Scripts.InfrastructureInterfaces.Services.SDCServices;
using Scripts.InfrastructureInterfaces.Spawners;
using Scripts.Presentation.Containers.GamePoints;
using Scripts.Presentation.Containers.HUDs;
using Scripts.Presentation.Containers.UI.Buttons;
using Scripts.Presentation.Containers.UI.Texts;
using Scripts.Presentation.Triggers.Taverns;
using Scripts.Presentation.Views;
using Scripts.Presentation.Views.GamePoints.VisitorsPoints;
using Scripts.Presentation.Views.Items.Coins;
using Scripts.Presentation.Views.Items.Garbages;
using Scripts.Presentation.Views.Player;
using Scripts.Presentation.Views.Player.Inventory;
using Scripts.Presentation.Views.Taverns.UpgradePoints;
using Scripts.Presentation.Views.Visitors;
using Scripts.PresentationInterfaces.Views.Items.Coins;
using Scripts.PresentationInterfaces.Views.Items.Garbages;
using Scripts.Utils.Repositories.CollectionRepository;
using Scripts.Utils.Repositories.ItemRepository;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.DIContainers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCameraView _playerCameraView;
        [SerializeField] private RootGamePoints _rootGamePoints;

        public override void InstallBindings()
        {
            Container.Bind<GamePlaySceneFactory>().AsSingle();

            Container.Bind<RootGamePoints>().FromInstance(_rootGamePoints).AsSingle();

            Container.Bind<VisitorPoints>().FromInstance(_rootGamePoints.VisitorPoints);

            HUD hud = Instantiate(Resources.Load<HUD>(PrefabPath.HUD));
            Container.Bind<HUD>().FromInstance(hud).AsSingle();

            Container.BindInterfacesAndSelfTo<AdvertisingService>().AsSingle();
            Container.Bind<LeanLocalization>().FromInstance(hud.LeanLocalization).AsSingle();
            Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();
            Container.Bind<ILeaderboardScoreSetter>().To<YandexLeaderboardScoreSetter>().AsSingle();

            Container.Bind<IMobilePlatformService>().To<MobilePlatformService>().AsSingle();

            Container.Bind<ContainerView>().FromInstance(hud.ContainerView).AsSingle();
            Container.BindInterfacesAndSelfTo<FormService>().AsSingle();

            Container.Bind<IGameOverService>().To<GameOverService>().AsSingle();

            Container.Bind<AdvertisingAfterCertainPeriodViewContainer>()
                .FromInstance(hud.AdvertisingAfterCertainPeriodViewContainer);
            Container.Bind<IAdvertisingAfterCertainPeriodService>()
                .To<AdvertisingAfterCertainPeriodService>().AsSingle();

            Container.Bind<LoadFormPresenterFactory>().AsSingle();
            Container.Bind<HudFormPresenterFactory>().AsSingle();
            Container.Bind<PauseMenuFormPresenterFactory>().AsSingle();
            Container.Bind<UpgradeFormPresenterFactory>().AsSingle();
            Container.Bind<TutorialFormPresenterFactory>().AsSingle();
            Container.Bind<GameOverFormPresenterFactory>().AsSingle();
            Container.Bind<SettingFormPresenterFactory>().AsSingle();

            Container.Bind<ISaveAfterCertainPeriodService>().To<SaveAfterCertainPeriodService>().AsSingle();

            Container.Bind<GameplayFormServiceFactory>().AsSingle();

            Container.Bind<AudioSourceUIPresenterFactory>().AsSingle();
            Container.Bind<AudioSourceUIFactory>().AsSingle();

            Container.Bind<FoodPickUpPointsViewFactory>().AsSingle();

            Container.Bind<PlayerInventorySlotsImages>().FromInstance(hud.PlayerInventorySlotsImages).AsSingle();

            Container.Bind<HudTextUIContainer>().FromInstance(hud.TextUIContainer).AsSingle();

            Container.Bind<CollectionRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradeProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<TavernProvider>().AsSingle();

            Container.Bind<IQuantityService>().To<VisitorQuantityService>().AsSingle();

            Container.Bind<VisitorSpawnService>().AsSingle();

            Container.Bind<IInputService>().To<InputService>().AsSingle();

            Container.Bind<VisitorPointsRepositoryFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<ItemProvider<IItem>>().AsSingle();

            Container.Bind<ItemsFactory>().AsSingle();

            Container.Bind<PlayerUpgradePresenterFactory>().AsSingle();
            Container.Bind<PlayerUpgradeViewFactory>().AsSingle();

            Container.Bind<EatPointPresenterFactory>().AsSingle();
            Container.Bind<EatPointViewFactory>().AsSingle();

            Container.Bind<SeatPointPresenterFactory>().AsSingle();
            Container.Bind<SeatPointViewFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<UpdateService>().AsSingle();

            Container.Bind<ICameraDirectionService>().To<CameraDirectionService>().AsSingle();

            Container.Bind<ItemViewFactory>().AsSingle();

            Container.Bind<ImageUIPresenterFactory>().AsSingle();
            Container.Bind<ImageUIFactory>().AsSingle();

            Container.Bind<ButtonUIPresenterFactory>().AsSingle();
            Container.Bind<ButtonUIFactory>().AsSingle();

            Container.Bind<TextUIPresenterFactory>().AsSingle();
            Container.Bind<TextUIFactory>().AsSingle();

            Container.Bind<ObjectPool<CoinView>>().AsSingle();
            Container.Bind<CoinPresenterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CoinViewFactory>().AsSingle();
            Container.Bind<ISpawner<ICoinView>>().To<CoinSpawner>().AsSingle();

            Container.Bind<TavernMoodPresenterFactory>().AsSingle();
            Container.Bind<TavernMoodViewFactory>().AsSingle();

            Container.Bind<ObjectPool<GarbageView>>().AsSingle();
            Container.Bind<GarbagePresenterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GarbageViewFactory>().AsSingle();
            Container.Bind<ISpawner<IGarbageView>>().To<GarbageSpawner>().AsSingle();

            Container.Bind<ObjectPool<VisitorView>>().AsSingle();
            Container.Bind<VisitorInventoryPresenterFactory>().AsSingle();
            Container.Bind<VisitorInventoryViewFactory>().AsSingle();
            Container.Bind<VisitorPresenterFactory>().AsSingle();
            Container.Bind<VisitorViewFactory>().AsSingle();

            Container.Bind<PlayerWalletPresenterFactory>().AsSingle();
            Container.Bind<PlayerWalletViewFactory>().AsSingle();

            Container.Bind<PlayerCameraView>().FromInstance(_playerCameraView).AsSingle();
            Container.Bind<PlayerCameraPresenterFactory>().AsSingle();
            Container.Bind<PlayerCameraViewFactory>().AsSingle();

            Container.Bind<PlayerInventoryPresenterFactory>().AsSingle();
            Container.Bind<PlayerInventoryViewFactory>().AsSingle();

            Container.Bind<PlayerMovementCharacteristic>().FromResource(
                PrefabPath.PlayerMovementCharacteristic);
            Container.Bind<IMovementService>().To<PlayerMovementService>().AsSingle();
            Container.Bind<PlayerMovementPresenterFactory>().AsSingle();
            Container.Bind<PlayerMovementViewFactory>().AsSingle();

            Container.Bind<PlayerViewFactory>().AsSingle();

            Container.Bind<TavernPickUpPointPresenterFactory>().AsSingle();
            Container.Bind<TavernFoodPickUpPointViewFactory>().AsSingle();

            Container.Bind<TavernUpgradeTrigger>().FromInstance(_rootGamePoints.TavernUpgradeTrigger).AsSingle();
            Container.Bind<TavernUpgradePointView>().FromInstance(hud.TavernUpgradePointView).AsSingle();
            Container.Bind<TavernUpgradePointService>().AsSingle();

            Container.Bind<PauseMenuButtonContainer>().FromInstance(hud.PauseMenuButtonContainer);
            Container.Bind<PauseMenuService>().AsSingle();

            Container.Bind<CreateService>().AsSingle();
            Container.Bind<LoadService>().AsSingle();
        }
    }
}