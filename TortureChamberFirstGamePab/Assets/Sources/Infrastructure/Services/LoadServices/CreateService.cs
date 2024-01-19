using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices
{
    public class CreateService : LoadServiceBase
    {
        public CreateService(PlayerMovementViewFactory playerMovementViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory, PlayerInventoryViewFactory playerInventoryViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory, TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory, 
            IUpgradeble playerCharismaUpgrader,
            IUpgradeble playerMovementUpgrader,
            IUpgradeble playerInventoryUpgrader
            ) :
            base(
                playerMovementViewFactory, playerCameraViewFactory, playerInventoryViewFactory,
                playerWalletViewFactory, textUIFactory, buttonUIFactory,
                playerCharismaUpgrader,
                playerMovementUpgrader,
                playerInventoryUpgrader
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
    }
}