using Domain.Constants;
using Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using DomainInterfaces.Items;
using Infrastructure.Factories.Controllers.Forms;
using Infrastructure.Factories.Controllers.Forms.Gameplays;
using Infrastructure.Factories.Controllers.Items.Coins;
using Infrastructure.Factories.Controllers.Items.Garbages;
using Infrastructure.Factories.Controllers.Players;
using Infrastructure.Factories.Controllers.Points;
using Infrastructure.Factories.Controllers.Taverns;
using Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Infrastructure.Factories.Controllers.UI;
using Infrastructure.Factories.Controllers.UI.AudioSources;
using Infrastructure.Factories.Controllers.Visitors;
using Infrastructure.Factories.Domains.Items;
using Infrastructure.Factories.Repositoryes;
using Infrastructure.Factories.Scenes;
using Infrastructure.Factories.Services.Forms;
using Infrastructure.Factories.Views.Items.Coins;
using Infrastructure.Factories.Views.Items.Common;
using Infrastructure.Factories.Views.Items.Garbeges;
using Infrastructure.Factories.Views.Players;
using Infrastructure.Factories.Views.Points;
using Infrastructure.Factories.Views.Taverns;
using Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Infrastructure.Factories.Views.UI;
using Infrastructure.Factories.Views.UI.AudioSources;
using Infrastructure.Factories.Views.Visitors;
using Infrastructure.Services;
using Infrastructure.Services.Cameras;
using Infrastructure.Services.Forms;
using Infrastructure.Services.LoadServices;
using Infrastructure.Services.Movement;
using Infrastructure.Services.ObjectPools;
using Infrastructure.Services.Providers.Players;
using Infrastructure.Services.Providers.Taverns;
using Infrastructure.Services.Providers.Upgrades;
using Infrastructure.Services.UpgradeServices;
using Infrastructure.Services.YandexSDCServices;
using Infrastructure.Spawners.Generic;
using InfrastructureInterfaces.Services;
using InfrastructureInterfaces.Services.Cameras;
using InfrastructureInterfaces.Services.InputServices;
using InfrastructureInterfaces.Services.Movement;
using InfrastructureInterfaces.Services.SDCServices;
using InfrastructureInterfaces.Spawners;
using Lean.Localization;
using Presentation.Containers.GamePoints;
using Presentation.Containers.HUDs;
using Presentation.Containers.UI.Buttons;
using Presentation.Containers.UI.Texts;
using Presentation.Triggers.Taverns;
using Presentation.Views;
using Presentation.Views.GamePoints.VisitorsPoints;
using Presentation.Views.Items.Coins;
using Presentation.Views.Items.Garbages;
using Presentation.Views.Player;
using Presentation.Views.Player.Inventory;
using Presentation.Views.Taverns.UpgradePoints;
using Presentation.Views.Visitors;
using PresentationInterfaces.Views.Items.Coins;
using PresentationInterfaces.Views.Items.Garbages;
using UnityEngine;
using Utils.Repositories.CollectionRepository;
using Utils.Repositories.ItemRepository;
using Zenject;

namespace Infrastructure.DIContainers
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