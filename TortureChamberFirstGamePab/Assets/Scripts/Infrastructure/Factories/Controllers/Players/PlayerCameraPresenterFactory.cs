using System;
using Scripts.Controllers.Player;
using Scripts.Domain.Players.PlayerCameras;
using Scripts.InfrastructureInterfaces.Services.InputServices;
using Scripts.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Infrastructure.Factories.Controllers.Players
{
    public class PlayerCameraPresenterFactory
    {
        private readonly IInputService _inputService;
        private readonly IUpdateServiceChanger _updateService;

        public PlayerCameraPresenterFactory(
            IInputService inputService,
            IUpdateServiceChanger updateService)
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

            return new PlayerCameraPresenter(
                playerCamera,
                playerCameraView,
                _inputService,
                _updateService);
        }
    }
}