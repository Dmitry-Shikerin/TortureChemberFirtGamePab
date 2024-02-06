using System;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.InfrastructureInterfaces.Factories;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Animations;
using Sources.Presentation.Views.Player;
using Sources.PresentationInterfaces.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerMovementViewFactory : IFactory<IView>
    {
        private readonly PlayerMovementPresenterFactory _playerMovementPresenterFactory;
        private readonly IPrefabFactory _prefabFactory;

        public PlayerMovementViewFactory
        (
            PlayerMovementPresenterFactory playerMovementPresenterFactory,
            IPrefabFactory prefabFactory
        )
        {
            _playerMovementPresenterFactory = playerMovementPresenterFactory ??
                                              throw new ArgumentNullException(nameof(playerMovementPresenterFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
        }

        public PlayerMovementView Create
        (
            PlayerMovement playerMovement,
            PlayerInventory playerInventory,
            PlayerMovementView playerMovementView,
            PlayerAnimation playerAnimation
        )
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
                _playerMovementPresenterFactory.Create(playerMovement,
                    playerMovementView, playerAnimation, playerInventory);

            playerMovementView.Construct(playerMovementPresenter);

            return playerMovementView;
        }
        
        public PlayerMovementView Create
        (
            PlayerMovement playerMovement,
            PlayerInventory playerInventory,
            PlayerAnimation playerAnimation
        )
        {
            if (playerMovement == null)
                throw new ArgumentNullException(nameof(playerMovement));

            if (playerInventory == null)
                throw new ArgumentNullException(nameof(playerInventory));
            
            if (playerAnimation == null)
                throw new ArgumentNullException(nameof(playerAnimation));

            PlayerMovementView playerMovementView = 
                _prefabFactory.Create<PlayerMovementView>("Prefabs/Player");
            
            PlayerMovementPresenter playerMovementPresenter =
                _playerMovementPresenterFactory.Create(playerMovement,
                    playerMovementView, playerAnimation, playerInventory);

            playerMovementView.Construct(playerMovementPresenter);

            return playerMovementView;
        }
    }
}