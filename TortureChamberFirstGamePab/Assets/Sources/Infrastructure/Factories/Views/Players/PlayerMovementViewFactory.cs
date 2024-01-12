using System;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.Presentation.Animations;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Controllers.Players;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerMovementViewFactory
    {
        private readonly PlayerMovementPresenterFactory _playerMovementPresenterFactory;

        public PlayerMovementViewFactory
        (
            PlayerMovementPresenterFactory playerMovementPresenterFactory
        )
        {
            _playerMovementPresenterFactory = playerMovementPresenterFactory ??
                                              throw new ArgumentNullException(nameof(playerMovementPresenterFactory));
        }

        public IPlayerMovementView Create(PlayerMovement playerMovement, 
            PlayerMovementView playerMovementView, PlayerAnimation playerAnimation,
            PlayerInventory playerInventory)
        {
            if (playerMovement == null)
                throw new ArgumentNullException(nameof(playerMovement));
            if (playerMovementView == null) 
                throw new ArgumentNullException(nameof(playerMovementView));
            if (playerAnimation == null) 
                throw new ArgumentNullException(nameof(playerAnimation));

            PlayerMovementPresenter playerMovementPresenter =
                _playerMovementPresenterFactory.Create(playerMovement,
                    playerMovementView, playerAnimation, playerInventory);
            
            playerMovementView.Construct(playerMovementPresenter);

            return playerMovementView;
        }
    }
}