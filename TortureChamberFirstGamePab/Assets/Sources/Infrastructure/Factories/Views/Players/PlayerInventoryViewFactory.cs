using System;
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

        public PlayerInventoryViewFactory(PlayerInventoryPresenterFactory playerInventoryPresenterFactory)
        {
            _playerInventoryPresenterFactory = 
                playerInventoryPresenterFactory ?? 
                throw new ArgumentNullException(nameof(playerInventoryPresenterFactory));
        }

        //TODO могу ли я ImageUIFactory прокинуть в презентер и создавать вьюшки там?
        public IPlayerInventoryView Create(PlayerInventoryView playerInventoryView,
            PlayerInventory playerInventory, ImageUIFactory imageUIFactory)
        {
            if (playerInventoryView == null) 
                throw new ArgumentNullException(nameof(playerInventoryView));
            if (playerInventory == null) 
                throw new ArgumentNullException(nameof(playerInventory));
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));
            
            imageUIFactory.Create(playerInventoryView.FirstSlotView.BackgroundImage);
            imageUIFactory.Create(playerInventoryView.FirstSlotView.Image);
            imageUIFactory.Create(playerInventoryView.SecondSlotView.BackgroundImage);
            imageUIFactory.Create(playerInventoryView.SecondSlotView.Image);
            imageUIFactory.Create(playerInventoryView.ThirdSlotView.BackgroundImage);
            imageUIFactory.Create(playerInventoryView.ThirdSlotView.Image);
            
            PlayerInventoryPresenter playerInventoryPresenter =
                _playerInventoryPresenterFactory.Create(playerInventoryView, playerInventory);
            
            playerInventoryView.Construct(playerInventoryPresenter);

            return playerInventoryView;
        }
    }
}