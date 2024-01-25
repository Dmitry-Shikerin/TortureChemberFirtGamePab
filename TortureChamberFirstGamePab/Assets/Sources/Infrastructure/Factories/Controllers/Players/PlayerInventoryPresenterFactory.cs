using System;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Infrastructure.Factories.Controllers.Players
{
    public class PlayerInventoryPresenterFactory
    {
        private readonly ItemViewFactory _itemViewFactory;
        private IUpgradeble _upgradeble;

        public PlayerInventoryPresenterFactory
        (
            IUpgradeProvider upgradeProvider,
            ItemViewFactory itemViewFactory
        )
        {
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _upgradeble = upgradeProvider.Inventory;
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
                _upgradeble
            );
        }
    }
}