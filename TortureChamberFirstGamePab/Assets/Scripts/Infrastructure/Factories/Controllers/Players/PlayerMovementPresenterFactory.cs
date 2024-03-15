using System;
using Scripts.Controllers.Player;
using Scripts.Domain.Players;
using Scripts.Domain.Players.PlayerMovements;
using Scripts.InfrastructureInterfaces.Services.Cameras;
using Scripts.InfrastructureInterfaces.Services.InputServices;
using Scripts.InfrastructureInterfaces.Services.Movement;
using Scripts.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Scripts.PresentationInterfaces.Animations;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Infrastructure.Factories.Controllers.Players
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