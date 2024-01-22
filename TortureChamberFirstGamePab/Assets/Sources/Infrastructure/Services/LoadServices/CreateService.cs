using Sources.Domain.GamePlays;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Domain.Taverns;
using Sources.Domain.Taverns.Data;
using Sources.Domain.Upgrades;
using Sources.Domain.Upgrades.Configs;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices
{
    public class CreateService : LoadServiceBase
    {
        public CreateService
        (
            PlayerMovementViewFactory playerMovementViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory,
            PlayerInventoryViewFactory playerInventoryViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory, 
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory,
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService
        ) :
            base
            (
                playerMovementViewFactory, playerCameraViewFactory, playerInventoryViewFactory,
                playerWalletViewFactory, textUIFactory, buttonUIFactory, playerDataService,
                playerUpgradeDataService, tavernDataService
            )
        {
        }

        protected override Player CreatePlayer()
        {
            Debug.Log("Create scene");

            PlayerMovement playerMovement = new PlayerMovement();
            PlayerInventory playerInventory = new PlayerInventory();
            PlayerWallet playerWallet = new PlayerWallet();

            Player player = new Player(playerMovement, playerInventory, playerWallet);

            return player;
        }

        protected override PlayerUpgrade CreatePlayerUpgrade()
        {
            UpgradeConfig charismaUpgradeConfig =
                Resources.Load<UpgradeConfig>("Configs/Upgrades/CharismaUpgradeConfig");
            UpgradeConfig inventoryUpgradeConfig =
                Resources.Load<UpgradeConfig>("Configs/Upgrades/InventoryUpgradeConfig");
            UpgradeConfig movementUpgradeConfig =
                Resources.Load<UpgradeConfig>("Configs/Upgrades/MovementUpgradeConfig");

            //TODO Отрефакторить запись
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