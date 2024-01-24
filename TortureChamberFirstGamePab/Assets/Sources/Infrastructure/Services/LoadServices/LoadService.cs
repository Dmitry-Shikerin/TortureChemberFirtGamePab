using System;
using Sources.Domain.Players;
using Sources.Domain.Taverns.Data;
using Sources.DomainInterfaces.Items;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.Brokers;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Voids;
using Sources.Utils.Repositoryes;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Services.LoadServices
{
    public class LoadService : LoadServiceBase
    {

        public LoadService
        (
            PlayerUpgradeViewFactory playerUpgradeViewFactory,
            HUD hud,
            DiContainer diContainer,
            ItemRepository<IItem> itemRepository,
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
            PlayerMovementUpgradeBrokerService playerMovementUpgradeBrokerService,
            PlayerInventoryUpgradeBrokerService playerInventoryUpgradeBrokerService
        ) :
            base
            (
                playerUpgradeViewFactory,
                hud,
                diContainer,
                itemRepository,
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
                playerMovementUpgradeBrokerService,
                playerInventoryUpgradeBrokerService
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