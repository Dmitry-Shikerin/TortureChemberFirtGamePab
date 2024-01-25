using System;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Presentation.UI.Conteiners;
using Sources.Presentation.Views.Player.Inventory;
using Sources.PresentationInterfaces.Views;
using Zenject;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerInventoryViewFactory : InfrastructureInterfaces.Factories.IFactory<IView>
    {
        private readonly PlayerInventoryPresenterFactory _playerInventoryPresenterFactory;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly HudTextUIContainer _hudTextUIContainer;

        public PlayerInventoryViewFactory
        (
            PlayerInventoryPresenterFactory playerInventoryPresenterFactory,
            ImageUIFactory imageUIFactory,
            HudTextUIContainer hudTextUIContainer
        )
        {
            _playerInventoryPresenterFactory =
                playerInventoryPresenterFactory ??
                throw new ArgumentNullException(nameof(playerInventoryPresenterFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _hudTextUIContainer = hudTextUIContainer ? hudTextUIContainer :
                throw new ArgumentNullException(nameof(hudTextUIContainer));
        }

        public PlayerInventoryView Create(PlayerInventory playerInventory, PlayerInventoryView playerInventoryView)
        {
            if (playerInventoryView == null)
                throw new ArgumentNullException(nameof(playerInventoryView));
            if (playerInventory == null)
                throw new ArgumentNullException(nameof(playerInventory));

            _imageUIFactory.Create(playerInventoryView.FirstSlotView.BackgroundImage);
            _imageUIFactory.Create(playerInventoryView.FirstSlotView.Image);
            _imageUIFactory.Create(playerInventoryView.SecondSlotView.BackgroundImage);
            _imageUIFactory.Create(playerInventoryView.SecondSlotView.Image);
            _imageUIFactory.Create(playerInventoryView.ThirdSlotView.BackgroundImage);
            _imageUIFactory.Create(playerInventoryView.ThirdSlotView.Image);

            PlayerInventoryPresenter playerInventoryPresenter =
                _playerInventoryPresenterFactory.Create(playerInventoryView, playerInventory,
                    _hudTextUIContainer.SystemErrorsText);

            playerInventoryView.Construct(playerInventoryPresenter);

            return playerInventoryView;
        }
    }
}