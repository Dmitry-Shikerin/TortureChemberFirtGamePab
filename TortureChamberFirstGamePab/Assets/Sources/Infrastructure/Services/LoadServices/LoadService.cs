using System;
using Sources.Domain.Players;
using Sources.Domain.Players.Data;
using Sources.Domain.Taverns.Data;
using Sources.DomainInterfaces.Items;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Infrastructure.Factories.Views.Taverns;
using Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Taverns;
using Sources.Presentation.Voids;
using Sources.Presentation.Voids.GamePoints;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.CollectionRepository;
using Sources.Utils.Repositoryes.ItemRepository;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Services.LoadServices
{
    public class LoadService : LoadServiceBase
    {
        public LoadService
        (
            PlayerCameraView playerCameraView,
            IPauseService pauseService,
            PauseMenuService pauseMenuService,
            CollectionRepository collectionRepository,
            EatPointViewFactory eatPointViewFactory,
            SeatPointViewFactory seatPointViewFactory,
            RootGamePoints rootGamePoints,
            IPlayerProviderSetter playerProviderSetter,
            ITavernProviderSetter tavernProviderSetter,
            IUpgradeProviderSetter upgradeProviderSetter,
            TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory,
            TavernMoodViewFactory tavernMoodViewFactory,
            PlayerUpgradeViewFactory playerUpgradeViewFactory,
            HUD hud,
            ItemProvider<IItem> itemProvider,
            PlayerMovementViewFactory playerMovementViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory,
            PlayerInventoryViewFactory playerInventoryViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory,
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService,
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory,
            ImageUIFactory imageUIFactory,
            IPrefabFactory prefabFactory,
            GameplayFormServiceFactory gameplayFormServiceFactory
        ) :
            base
            (
                playerCameraView,
                pauseService,
                pauseMenuService,
                collectionRepository,
                eatPointViewFactory,
                seatPointViewFactory,
                rootGamePoints,
                playerProviderSetter,
                tavernProviderSetter,
                upgradeProviderSetter,
                tavernFoodPickUpPointViewFactory,
                tavernMoodViewFactory,
                playerUpgradeViewFactory,
                hud,
                itemProvider,
                playerMovementViewFactory,
                playerCameraViewFactory,
                playerInventoryViewFactory,
                playerWalletViewFactory,
                textUIFactory,
                buttonUIFactory,
                playerDataService,
                playerUpgradeDataService,
                tavernDataService,
                imageUIFactory,
                prefabFactory,
                gameplayFormServiceFactory
            )
        {
        }

        protected override Player CreatePlayer()
        {
            Debug.Log("Load scene");
            return PlayerDataService.Load();
        }

        protected override PlayerUpgrade CreatePlayerUpgrade() => 
            PlayerUpgradeDataService.Load();

        protected override Tavern CreateTavern() => 
            TavernDataService.Load();
    }
}