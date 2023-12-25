using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.Presentation.Animations;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Animations;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Cameras;

namespace MyProject.Sources.Infrastructure.Factorys.Controllers
{
    public class PlayerMovementPresenterFactory
    {
        private readonly InputService _inputService;
        private readonly UpdateService _updateService;
        private readonly CameraDirectionService _cameraDirectionService;

        public PlayerMovementPresenterFactory
        (
            InputService inputService,
            UpdateService updateService,
            CameraDirectionService cameraDirectionService
        )
        {
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? 
                             throw new ArgumentNullException(nameof(updateService));
            _cameraDirectionService = cameraDirectionService ?? 
                                      throw new ArgumentNullException(nameof(cameraDirectionService));
        }

        public PlayerMovementPresenter Create(PlayerMovement playerMovement,
            IPlayerMovementView playerMovementView, IPlayerAnimation playerAnimation)
        {
            if (playerMovement == null)
                throw new ArgumentNullException(nameof(playerMovement));
            if (playerMovementView == null)
                throw new ArgumentNullException(nameof(playerMovementView));
            if (playerAnimation == null) 
                throw new ArgumentNullException(nameof(playerAnimation));

            return new PlayerMovementPresenter
            (
                playerMovementView,
                playerAnimation,
                playerMovement,
                _inputService,
                _updateService,
                _cameraDirectionService
            );
        }
    }
}