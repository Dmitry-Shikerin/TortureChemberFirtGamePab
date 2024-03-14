using System;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Player.Inventory;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerInventoryViewFactory
    {
        private readonly HudTextUIContainer _hudTextUIContainer;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly PlayerInventoryPresenterFactory _playerInventoryPresenterFactory;
        private readonly PlayerInventorySlotsImages _playerInventorySlotsImages;

        public PlayerInventoryViewFactory(
            PlayerInventoryPresenterFactory playerInventoryPresenterFactory,
            ImageUIFactory imageUIFactory,
            HudTextUIContainer hudTextUIContainer,
            PlayerInventorySlotsImages playerInventorySlotsImages)
        {
            _playerInventoryPresenterFactory =
                playerInventoryPresenterFactory ??
                throw new ArgumentNullException(nameof(playerInventoryPresenterFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _playerInventorySlotsImages = playerInventorySlotsImages
                ? playerInventorySlotsImages
                : throw new ArgumentNullException(nameof(playerInventorySlotsImages));
            _hudTextUIContainer = hudTextUIContainer
                ? hudTextUIContainer
                : throw new ArgumentNullException(nameof(hudTextUIContainer));
        }

        public PlayerInventoryView Create(
            PlayerInventory playerInventory,
            PlayerInventoryView playerInventoryView)
        {
            if (playerInventoryView == null)
                throw new ArgumentNullException(nameof(playerInventoryView));
            if (playerInventory == null)
                throw new ArgumentNullException(nameof(playerInventory));

            _imageUIFactory.Create(_playerInventorySlotsImages.FirsSlotImage);
            _imageUIFactory.Create(_playerInventorySlotsImages.FirsSlotBackgroundImage);
            _imageUIFactory.Create(_playerInventorySlotsImages.SecondSlotImage);
            _imageUIFactory.Create(_playerInventorySlotsImages.SecondSlotBackgroundImage);
            _imageUIFactory.Create(_playerInventorySlotsImages.ThirdSlotImage);
            _imageUIFactory.Create(_playerInventorySlotsImages.ThirdSlotBackgroundImage);

            playerInventoryView.FirstSlotView.Construct(_playerInventorySlotsImages.FirsSlotImage,
                _playerInventorySlotsImages.FirsSlotBackgroundImage);
            playerInventoryView.SecondSlotView.Construct(_playerInventorySlotsImages.SecondSlotImage,
                _playerInventorySlotsImages.SecondSlotBackgroundImage);
            playerInventoryView.ThirdSlotView.Construct(_playerInventorySlotsImages.ThirdSlotImage,
                _playerInventorySlotsImages.ThirdSlotBackgroundImage);

            var playerInventoryPresenter =
                _playerInventoryPresenterFactory.Create(_playerInventorySlotsImages,
                    playerInventoryView,
                    playerInventory,
                    _hudTextUIContainer.SystemErrorsText);

            playerInventoryView.Construct(playerInventoryPresenter);

            return playerInventoryView;
        }
    }
}