using System;
using Scripts.Controllers.Player;
using Scripts.Domain.Players;
using Scripts.Domain.Players.PlayerMovements;
using Scripts.Infrastructure.Factories.Controllers.Players;
using Scripts.Presentation.Animations;
using Scripts.Presentation.Views.Player;

namespace Scripts.Infrastructure.Factories.Views.Players
{
    public class PlayerMovementViewFactory
    {
        private readonly PlayerMovementPresenterFactory _playerMovementPresenterFactory;

        public PlayerMovementViewFactory(PlayerMovementPresenterFactory playerMovementPresenterFactory)
        {
            _playerMovementPresenterFactory = playerMovementPresenterFactory ??
                                              throw new ArgumentNullException(nameof(playerMovementPresenterFactory));
        }

        public PlayerMovementView Create(
            PlayerMovement playerMovement,
            PlayerInventory playerInventory,
            PlayerMovementView playerMovementView,
            PlayerAnimation playerAnimation)
        {
            if (playerMovement == null)
                throw new ArgumentNullException(nameof(playerMovement));

            if (playerInventory == null)
                throw new ArgumentNullException(nameof(playerInventory));

            if (playerMovementView == null)
                throw new ArgumentNullException(nameof(playerMovementView));

            if (playerAnimation == null)
                throw new ArgumentNullException(nameof(playerAnimation));

            PlayerMovementPresenter playerMovementPresenter =
                _playerMovementPresenterFactory.Create(
                    playerMovement, playerMovementView, playerAnimation, playerInventory);

            playerMovementView.Construct(playerMovementPresenter);

            return playerMovementView;
        }
    }
}