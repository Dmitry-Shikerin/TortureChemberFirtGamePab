using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Presentation.UI;
using Sources.Presentation.Views.Player;
using Sources.PresentationInterfaces.UI;

namespace MyProject.Sources.Infrastructure.Factorys.Views
{
    public class PlayerInventoryViewFactory
    {
        private readonly PlayerInventoryPresenterFactory _playerInventoryPresenterFactory;

        public PlayerInventoryViewFactory(PlayerInventoryPresenterFactory playerInventoryPresenterFactory)
        {
            _playerInventoryPresenterFactory = 
                playerInventoryPresenterFactory ?? 
                throw new ArgumentNullException(nameof(playerInventoryPresenterFactory));
        }

        public IPlayerInventoryView Create(PlayerInventoryView playerInventoryView,
            ITextUI textUI, PlayerInventory playerInventory, ItemViewFactory itemViewFactory,
            ImageUIFactory imageUIFactory)
        {
            if (playerInventoryView == null) 
                throw new ArgumentNullException(nameof(playerInventoryView));
            if (textUI == null) 
                throw new ArgumentNullException(nameof(textUI));
            if (playerInventory == null) 
                throw new ArgumentNullException(nameof(playerInventory));
            if (itemViewFactory == null) 
                throw new ArgumentNullException(nameof(itemViewFactory));
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));
            
            imageUIFactory.Create(playerInventoryView.FirstSlotView.BackgroundImage);
            imageUIFactory.Create(playerInventoryView.FirstSlotView.Image);
            imageUIFactory.Create(playerInventoryView.SecondSlotView.BackgroundImage);
            imageUIFactory.Create(playerInventoryView.SecondSlotView.Image);
            imageUIFactory.Create(playerInventoryView.ThirdSlotView.BackgroundImage);
            imageUIFactory.Create(playerInventoryView.ThirdSlotView.Image);
            
            PlayerInventoryPresenter playerInventoryPresenter =
                _playerInventoryPresenterFactory.Create(playerInventoryView,
                    textUI, playerInventory, itemViewFactory);
            
            playerInventoryView.Construct(playerInventoryPresenter);

            return playerInventoryView;
        }
    }
}