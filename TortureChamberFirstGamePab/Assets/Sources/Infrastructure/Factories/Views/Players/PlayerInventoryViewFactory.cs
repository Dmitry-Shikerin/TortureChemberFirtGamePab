using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Views.Items.Common;
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
            ITextUI textUI, PlayerInventory playerInventory, ItemViewFactory itemViewFactory)
        {
            PlayerInventoryPresenter playerInventoryPresenter =
                _playerInventoryPresenterFactory.Create(playerInventoryView,
                    textUI, playerInventory, itemViewFactory);
            
            playerInventoryView.Construct(playerInventoryPresenter);

            return playerInventoryView;
        }
    }
}