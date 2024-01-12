using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players.PlayerCameras;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factories.Services;

namespace MyProject.Sources.Infrastructure.Factorys.Controllers
{
    public class PlayerCameraPresenterFactory
    {
        private readonly InputService _inputService;
        private readonly UpdateService _updateService;

        public PlayerCameraPresenterFactory(InputService inputService, [NotNull] UpdateService updateService)
        {
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? 
                             throw new ArgumentNullException(nameof(updateService));
        }

        public PlayerCameraPresenter Create(PlayerCamera playerCamera, IPlayerCameraView playerCameraView)
        {
            if (playerCamera == null) 
                throw new ArgumentNullException(nameof(playerCamera));
            if (playerCameraView == null) 
                throw new ArgumentNullException(nameof(playerCameraView));
            
            return new PlayerCameraPresenter
            (
                playerCamera,
                playerCameraView,
                _inputService,
                _updateService
            );
        }
    }
}