using System;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.GamePlays;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Domain.Taverns;
using Sources.Domain.Upgrades;
using Sources.Domain.Upgrades.Configs;
using Sources.DomainInterfaces.Items;
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
using Sources.Presentation.Voids;
using Sources.Presentation.Voids.GamePoints;
using Sources.Utils.Repositoryes.CollectionRepository;
using Sources.Utils.Repositoryes.ItemRepository;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Services.LoadServices
{
    public class CreateService : LoadServiceBase
    {
        public CreateService
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
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory,
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService,
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
        {}
        
        protected override Player CreatePlayer()
        {
            PlayerMovement playerMovement = new PlayerMovement();
            PlayerInventory playerInventory = new PlayerInventory();
            PlayerWallet playerWallet = new PlayerWallet();

            Player player = new Player(playerMovement, playerInventory, playerWallet);

            return player;
        }

        protected override PlayerUpgrade CreatePlayerUpgrade()
        {
            UpgradeConfig charismaUpgradeConfig =
                Resources.Load<UpgradeConfig>(Constant.UpgradeConfigPath.Charisma);
            UpgradeConfig inventoryUpgradeConfig =
                Resources.Load<UpgradeConfig>(Constant.UpgradeConfigPath.Inventory);
            UpgradeConfig movementUpgradeConfig =
                Resources.Load<UpgradeConfig>(Constant.UpgradeConfigPath.Movement);

            Upgrader charismaUpgrader = new Upgrader(charismaUpgradeConfig);
            Upgrader inventoryUpgrader = new Upgrader(inventoryUpgradeConfig);
            Upgrader movementUpgrader = new Upgrader(movementUpgradeConfig);

            PlayerUpgrade playerUpgrade = new PlayerUpgrade(charismaUpgrader,
                inventoryUpgrader, movementUpgrader);

            return playerUpgrade;
        }

        protected override Tavern CreateTavern()
        {
            TavernMood tavernMood = new TavernMood();
            GamePlay gamePlay = new GamePlay();
            
            Tavern tavern = new Tavern(tavernMood, gamePlay);

            return tavern;
        }
    }
}