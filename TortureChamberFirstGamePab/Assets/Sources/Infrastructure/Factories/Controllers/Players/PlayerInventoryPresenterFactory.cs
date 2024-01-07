using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.PresentationInterfaces.UI;

namespace MyProject.Sources.Infrastructure.Factorys.Controllers
{
    public class PlayerInventoryPresenterFactory
    {
        public PlayerInventoryPresenter Create
        (
            IPlayerInventoryView playerInventoryView,
            ITextUI textUI,
            PlayerInventory playerInventory,
            ItemViewFactory itemViewFactory
        )
        {
            if (playerInventoryView == null) 
                throw new ArgumentNullException(nameof(playerInventoryView));
            if (textUI == null) 
                throw new ArgumentNullException(nameof(textUI));
            if (playerInventory == null) 
                throw new ArgumentNullException(nameof(playerInventory));
            if (itemViewFactory == null) 
                throw new ArgumentNullException(nameof(itemViewFactory));
            
            return new PlayerInventoryPresenter
            (
                playerInventoryView,
                textUI,
                playerInventory,
                itemViewFactory
            );
        }
    }
}