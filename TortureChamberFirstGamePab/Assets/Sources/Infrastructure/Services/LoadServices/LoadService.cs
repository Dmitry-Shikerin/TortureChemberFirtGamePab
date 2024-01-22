using System;
using Sources.Domain.Players;
using Sources.Domain.Taverns.Data;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices
{
    public class LoadService : LoadServiceBase
    {
        public LoadService
        (
            PlayerMovementViewFactory playerMovementViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory,
            PlayerInventoryViewFactory playerInventoryViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory,
            IDataService<Player> playerDataService,
            IDataService<PlayerUpgrade> playerUpgradeDataService,
            IDataService<Tavern> tavernDataService,
            TextUIFactory textUIFactory,
            ButtonUIFactory buttonUIFactory
        ) :
            base
            (
                playerMovementViewFactory,
                playerCameraViewFactory,
                playerInventoryViewFactory,
                playerWalletViewFactory,
                textUIFactory,
                buttonUIFactory,
                playerDataService,
                playerUpgradeDataService,
                tavernDataService
            )
        {
        }

        protected override Player CreatePlayer()
        {
            Debug.Log("Load scene");
            return PlayerDataService.Load();
        }

        protected override PlayerUpgrade CreatePlayerUpgrade()
        {
            return PlayerUpgradeDataService.Load();
        }

        protected override Tavern CreateTavern()
        {
            return TavernDataService.Load();
        }
    }
}