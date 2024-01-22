using System;
using JetBrains.Annotations;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.InfrastructureInterfaces.Factories;
using Sources.Presentation.Views.Player.Inventory;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Players;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerInventoryViewFactory : IFactory<IView>
    {
        private readonly PlayerInventoryPresenterFactory _playerInventoryPresenterFactory;
        private readonly ImageUIFactory _imageUIFactory;

        public PlayerInventoryViewFactory(
            PlayerInventoryPresenterFactory playerInventoryPresenterFactory,
            ImageUIFactory imageUIFactory)
        {
            _playerInventoryPresenterFactory = 
                playerInventoryPresenterFactory ?? 
                throw new ArgumentNullException(nameof(playerInventoryPresenterFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
        }

        public PlayerInventoryView Create(
            PlayerInventory playerInventory, PlayerInventoryView playerInventoryView)
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
                _playerInventoryPresenterFactory.Create(playerInventoryView, playerInventory);
            
            playerInventoryView.Construct(playerInventoryPresenter);

            return playerInventoryView;
        }
    }
}