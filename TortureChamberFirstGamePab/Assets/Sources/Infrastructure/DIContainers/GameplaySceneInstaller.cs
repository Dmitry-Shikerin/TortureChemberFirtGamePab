﻿using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Sources.Domain.Taverns.Data;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Builders;
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
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Brokers;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.Movement;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.Presentation.Triggers.Taverns;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Items.Coins;
using Sources.Presentation.Views.Items.Garbages;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using Sources.Presentation.Views.UIs;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids;
using Sources.Presentation.Voids.GamePoints;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.CollectionRepository;
using Zenject;

namespace Sources.Infrastructure.DIContainers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //TODO исправить
            RootGamePoints rootGamePoints = FindObjectOfType<RootGamePoints>(true);
            Container.Bind<RootGamePoints>().FromInstance(rootGamePoints).AsSingle();

            //TODO исправить
            HUD hud = FindObjectOfType<HUD>();
            Container.Bind<HUD>().FromInstance(hud).AsSingle();
            
            HudTextUIContainer hudTextUIContainer = hud.GetComponent<HudTextUIContainer>();
            Container.Bind<HudTextUIContainer>().FromInstance(hudTextUIContainer).AsSingle();

            Container.Bind<CollectionRepository>().AsSingle();

            Container.Bind<IInputService>().To<InputService>().AsSingle();

            Container.Bind<VisitorPointRepositoryFactory>().AsSingle();

            Container.Bind<ItemRepository<IItem>>().AsSingle();

            Container.Bind<ItemsFactory>().AsSingle();

            Container.Bind<ProductShuffleService>().AsSingle();

            Container.Bind<PlayerUpgradePresenterFactory>().AsSingle();
            Container.Bind<PlayerUpgradeViewFactory>().AsSingle();

            Container.Bind<EatPointPresenterFactory>().AsSingle();
            Container.Bind<EatPointViewFactory>().AsSingle();

            Container.Bind<SeatPointPresenterFactory>().AsSingle();
            Container.Bind<SeatPointViewFactory>().AsSingle();

            Container.Bind<UpdateService>().AsSingle();


            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();

            Container.Bind<CameraDirectionService>().AsSingle();

            Container.Bind<ItemViewFactory>().AsSingle();

            Container.Bind<ImageUIPresenterFactory>().AsSingle();
            Container.Bind<ImageUIFactory>().AsSingle();

            Container.Bind<ButtonUIPresenterFactory>().AsSingle();
            Container.Bind<ButtonUIFactory>().AsSingle();

            Container.Bind<TextUIPresenterFactory>().AsSingle();
            Container.Bind<TextUIFactory>().AsSingle();

            Container.Bind<ObjectPool<CoinAnimationView>>().AsSingle();
            Container.Bind<CoinAnimationPresenterFactory>().AsSingle();
            Container.Bind<CoinAnimationViewFactory>().AsSingle();
            Container.Bind<CoinSpawner>().AsSingle();

            Container.Bind<TavernMoodPresenterFactory>().AsSingle();
            Container.Bind<TavernMoodViewFactory>().AsSingle();

            Container.Bind<ObjectPool<GarbageView>>().AsSingle();
            Container.Bind<GarbagePresenterFactory>().AsSingle();
            Container.Bind<GarbageViewFactory>().AsSingle();
            Container.Bind<GarbageSpawner>().AsSingle();

            Container.Bind<ObjectPool<VisitorView>>().AsSingle();
            Container.Bind<VisitorPresenterFactory>().AsSingle();
            Container.Bind<VisitorViewFactory>().AsSingle();

            Container.Bind<PlayerWalletPresenterFactory>().AsSingle();
            Container.Bind<PlayerWalletViewFactory>().AsSingle();

            //TODO плохо
            PlayerCameraView playerCameraView = FindObjectOfType<PlayerCameraView>();
            Container.Bind<PlayerCameraView>().FromInstance(playerCameraView).AsSingle();
            Container.Bind<PlayerCameraPresenterFactory>().AsSingle();
            Container.Bind<PlayerCameraViewFactory>().AsSingle();

            Container.Bind<PlayerInventoryUpgradeBrokerService>().AsSingle();
            Container.Bind<PlayerInventoryPresenterFactory>().AsSingle();
            Container.Bind<PlayerInventoryViewFactory>().AsSingle();

            //TODO подправить
            Container.Bind<PlayerMovementCharacteristic>().FromResource("Configs/PlayerMovementCharacteristics");
            Container.Bind<PlayerMovementUpgradeBrokerService>().AsSingle();
            Container.Bind<PlayerMovementService>().AsSingle();
            Container.Bind<PlayerMovementPresenterFactory>().AsSingle();
            Container.Bind<PlayerMovementViewFactory>().AsSingle();

            Container.Bind<TavernPickUpPointPresenterFactory>().AsSingle();
            Container.Bind<TavernFoodPickUpPointViewFactory>().AsSingle();

            //TODO исправить
            TavernUpgradeTrigger tavernUpgradeTrigger = FindObjectOfType<TavernUpgradeTrigger>(true);
            TavernUpgradePointView tavernUpgradePointView = FindObjectOfType<TavernUpgradePointView>(true);
            Container.Bind<TavernUpgradeTrigger>().FromInstance(tavernUpgradeTrigger);
            Container.Bind<TavernUpgradePointView>().FromInstance(tavernUpgradePointView);
            Container.Bind<TavernUpgradePointService>().AsSingle();

            //TODO исправить
            PauseMenuWindow pauseMenuWindow = hud.GetComponentInChildren<PauseMenuWindow>(true);
            Container.Bind<PauseMenuWindow>().FromInstance(pauseMenuWindow);
            Container.Bind<PauseMenuService>().AsSingle();

            //TODO плохо, взять на заметку
            Container.Bind<IDataService<Player>>().To<PlayerDataService>().AsSingle();
            Container.Bind<IDataService<PlayerUpgrade>>().To<PlayerUpgradeDataService>().AsSingle();
            Container.Bind<IDataService<Tavern>>().To<TavernDataService>().AsSingle();
            Container.Bind<CreateService>().AsSingle();
            Container.Bind<LoadService>().AsSingle();
        }
    }
}