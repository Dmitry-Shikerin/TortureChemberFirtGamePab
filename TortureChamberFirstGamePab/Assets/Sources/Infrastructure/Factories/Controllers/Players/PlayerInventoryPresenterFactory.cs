using System;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Services.Brokers;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Infrastructure.Factories.Controllers.Players
{
    public class PlayerInventoryPresenterFactory
    {
        private readonly PlayerInventoryUpgradeBrokerService _playerInventoryUpgradeBrokerService;
        private readonly ItemViewFactory _itemViewFactory;

        public PlayerInventoryPresenterFactory(
            PlayerInventoryUpgradeBrokerService playerInventoryUpgradeBrokerService,
            ItemViewFactory itemViewFactory)
        {
            _playerInventoryUpgradeBrokerService = playerInventoryUpgradeBrokerService ?? throw new ArgumentNullException(nameof(playerInventoryUpgradeBrokerService));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
        }
        public PlayerInventoryPresenter Create
        (
            IPlayerInventoryView playerInventoryView,
            PlayerInventory playerInventory,
            ITextUI textUI
        )
        {
            if (playerInventoryView == null) 
                throw new ArgumentNullException(nameof(playerInventoryView));
            if (playerInventory == null) 
                throw new ArgumentNullException(nameof(playerInventory));
            
            return new PlayerInventoryPresenter
            (
                playerInventoryView,
                textUI,
                playerInventory,
                _itemViewFactory,
                _playerInventoryUpgradeBrokerService.PlayerInventoryUpgrader
            );
        }
    }
}