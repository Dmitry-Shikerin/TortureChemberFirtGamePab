using System;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.Presentation.Views.Player.Inventory;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Infrastructure.Factories.Controllers.Players
{
    public class PlayerInventoryPresenterFactory
    {
        private readonly IUpgradeProvider _upgradeProvider;
        private readonly ItemViewFactory _itemViewFactory;

        public PlayerInventoryPresenterFactory
        (
            IUpgradeProvider upgradeProvider,
            ItemViewFactory itemViewFactory
        )
        {
            _upgradeProvider = upgradeProvider ?? throw new ArgumentNullException(nameof(upgradeProvider));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
        }

        public PlayerInventoryPresenter Create
        (
            PlayerInventorySlotsImages playerInventorySlotsImages,
            IPlayerInventoryView playerInventoryView,
            PlayerInventory playerInventory,
            ITextUI textUI
        )
        {
            if (playerInventorySlotsImages == null) 
                throw new ArgumentNullException(nameof(playerInventorySlotsImages));
            
            if (playerInventoryView == null)
                throw new ArgumentNullException(nameof(playerInventoryView));
            
            if (playerInventory == null)
                throw new ArgumentNullException(nameof(playerInventory));
            
            if (textUI == null)
                throw new ArgumentNullException(nameof(textUI));

            return new PlayerInventoryPresenter
            (
                playerInventorySlotsImages,
                playerInventoryView,
                textUI,
                playerInventory,
                _itemViewFactory,
                _upgradeProvider.Inventory
            );
        }
    }
}