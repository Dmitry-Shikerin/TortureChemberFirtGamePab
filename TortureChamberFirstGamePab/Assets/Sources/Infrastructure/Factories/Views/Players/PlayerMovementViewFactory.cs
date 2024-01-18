using System;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.InfrastructureInterfaces.Factories;
using Sources.Presentation.Animations;
using Sources.Presentation.Views.Player;
using Sources.PresentationInterfaces.Views;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerMovementViewFactory : IFactory<IView>
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

        public PlayerMovementView Create(PlayerMovement playerMovement, PlayerInventory playerInventory)
        {
            if (playerMovement == null)
                throw new ArgumentNullException(nameof(playerMovement));

            //TODO потом исправить
            PlayerMovementView playerMovementView = Object.FindObjectOfType<PlayerMovementView>();
            PlayerAnimation playerAnimation = playerMovementView.GetComponent<PlayerAnimation>();
            
            PlayerMovementPresenter playerMovementPresenter =
                _playerMovementPresenterFactory.Create(playerMovement,
                    playerMovementView, playerAnimation, playerInventory);
            
            playerMovementView.Construct(playerMovementPresenter);

            return playerMovementView;
        }
    }
}