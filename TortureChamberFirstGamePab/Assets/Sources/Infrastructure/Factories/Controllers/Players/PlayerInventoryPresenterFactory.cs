using System;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Infrastructure.Factories.Controllers.Players
{
    public class PlayerInventoryPresenterFactory
    {
        private readonly IUpgradeble _upgradeble;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly ITextUI _textUI;

        public PlayerInventoryPresenterFactory(IUpgradeble upgradeble,
            ItemViewFactory itemViewFactory, ITextUI textUI)
        {
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
        }
        public PlayerInventoryPresenter Create
        (
            IPlayerInventoryView playerInventoryView,
            PlayerInventory playerInventory
        )
        {
            if (playerInventoryView == null) 
                throw new ArgumentNullException(nameof(playerInventoryView));
            if (playerInventory == null) 
                throw new ArgumentNullException(nameof(playerInventory));
            
            return new PlayerInventoryPresenter
            (
                playerInventoryView,
                _textUI,
                playerInventory,
                _itemViewFactory,
                _upgradeble
            );
        }
    }
}