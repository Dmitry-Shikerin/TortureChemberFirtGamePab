using System;
using Scripts.Controllers.Player;
using Scripts.Domain.Players;
using Scripts.Infrastructure.Factories.Views.Items.Common;
using Scripts.InfrastructureInterfaces.Services.Providers.Upgrades;
using Scripts.PresentationInterfaces.UI;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Infrastructure.Factories.Controllers.Players
{
    public class PlayerInventoryPresenterFactory
    {
        private readonly ItemViewFactory _itemViewFactory;
        private readonly IUpgradeProvider _upgradeProvider;

        public PlayerInventoryPresenterFactory(
            IUpgradeProvider upgradeProvider,
            ItemViewFactory itemViewFactory)
        {
            _upgradeProvider = upgradeProvider ?? throw new ArgumentNullException(nameof(upgradeProvider));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
        }

        public PlayerInventoryPresenter Create(
            IPlayerInventoryView playerInventoryView,
            PlayerInventory playerInventory,
            ITextUI textUI)
        {
            if (playerInventoryView == null)
                throw new ArgumentNullException(nameof(playerInventoryView));

            if (playerInventory == null)
                throw new ArgumentNullException(nameof(playerInventory));

            if (textUI == null)
                throw new ArgumentNullException(nameof(textUI));

            return new PlayerInventoryPresenter(
                playerInventoryView,
                textUI,
                playerInventory,
                _itemViewFactory,
                _upgradeProvider.Inventory);
        }
    }
}