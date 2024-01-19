using System;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices
{
    public class LoadService : LoadServiceBase
    {
        private readonly IPlayerDataService _playerDataService;

        public LoadService( 
            PlayerMovementViewFactory playerMovementViewFactory, 
            PlayerCameraViewFactory playerCameraViewFactory, 
            PlayerInventoryViewFactory playerInventoryViewFactory, 
            PlayerWalletViewFactory playerWalletViewFactory,
            IPlayerDataService playerDataService,
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory,
            IUpgradeble playerCharismaUpgrader,
            IUpgradeble playerMovementUpgrader,
            IUpgradeble playerInventoryUpgrader
            ) :
            base(playerMovementViewFactory, 
                playerCameraViewFactory, playerInventoryViewFactory, 
                playerWalletViewFactory, textUIFactory,buttonUIFactory,
                 playerCharismaUpgrader,
                 playerMovementUpgrader,
                playerInventoryUpgrader
                )
        {
            _playerDataService = playerDataService ?? throw new ArgumentNullException(nameof(playerDataService));
        }

        protected override Player CreatePlayer()
        {
            Debug.Log("Load scene");
            return _playerDataService.LoadPlayer();
        }
    }
}