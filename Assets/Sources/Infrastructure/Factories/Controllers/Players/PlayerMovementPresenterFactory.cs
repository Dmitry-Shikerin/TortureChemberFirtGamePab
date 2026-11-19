using System;
using Controllers.Player;
using Domain.Players;
using Domain.Players.PlayerMovements;
using InfrastructureInterfaces.Services.Cameras;
using InfrastructureInterfaces.Services.InputServices;
using InfrastructureInterfaces.Services.Movement;
using InfrastructureInterfaces.Services.UpdateServices.Changer;
using PresentationInterfaces.Animations;
using PresentationInterfaces.Views.Players;

namespace Infrastructure.Factories.Controllers.Players
{
    public class PlayerMovementPresenterFactory
    {
        private readonly ICameraDirectionService _cameraDirectionService;
        private readonly IInputService _inputService;
        private readonly IMovementService _playerMovementService;
        private readonly IUpdateServiceChanger _updateService;

        public PlayerMovementPresenterFactory(
            IInputService inputService,
            IUpdateServiceChanger updateService,
            ICameraDirectionService cameraDirectionService,
            IMovementService playerMovementService)
        {
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ??
                             throw new ArgumentNullException(nameof(updateService));
            _cameraDirectionService = cameraDirectionService ??
                                      throw new ArgumentNullException(nameof(cameraDirectionService));
            _playerMovementService = playerMovementService ??
                                     throw new ArgumentNullException(nameof(playerMovementService));
        }

        public PlayerMovementPresenter Create(
            PlayerMovement playerMovement,
            IPlayerMovementView playerMovementView,
            IPlayerAnimation playerAnimation,
            PlayerInventory playerInventory)
        {
            if (playerMovement == null)
                throw new ArgumentNullException(nameof(playerMovement));

            if (playerMovementView == null)
                throw new ArgumentNullException(nameof(playerMovementView));

            if (playerAnimation == null)
                throw new ArgumentNullException(nameof(playerAnimation));

            if (playerInventory == null)
                throw new ArgumentNullException(nameof(playerInventory));

            return new PlayerMovementPresenter(
                playerMovementView,
                playerAnimation,
                playerMovement,
                _inputService,
                _updateService,
                _cameraDirectionService,
                playerInventory,
                _playerMovementService);
        }
    }
}