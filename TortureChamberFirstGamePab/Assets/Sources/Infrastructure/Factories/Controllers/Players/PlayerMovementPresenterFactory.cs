using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.InfrastructureInterfaces.Services.Cameras;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.Movement;
using Sources.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Sources.PresentationInterfaces.Animations;

namespace Sources.Infrastructure.Factories.Controllers.Players
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