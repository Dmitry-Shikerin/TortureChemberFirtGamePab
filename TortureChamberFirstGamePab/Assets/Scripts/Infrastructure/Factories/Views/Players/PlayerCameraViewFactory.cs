using System;
using Scripts.Controllers.Player;
using Scripts.Domain.Players.PlayerCameras;
using Scripts.Infrastructure.Factories.Controllers.Players;
using Scripts.Presentation.Views.Player;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Infrastructure.Factories.Views.Players
{
    public class PlayerCameraViewFactory
    {
        private readonly PlayerCameraPresenterFactory _playerCameraPresenterFactory;

        public PlayerCameraViewFactory(PlayerCameraPresenterFactory playerCameraPresenterFactory)
        {
            _playerCameraPresenterFactory = playerCameraPresenterFactory ??
                                            throw new ArgumentNullException(nameof(playerCameraPresenterFactory));
        }

        public IPlayerCameraView Create(PlayerCamera playerCamera, PlayerCameraView playerCameraView)
        {
            if (playerCamera == null)
                throw new ArgumentNullException(nameof(playerCamera));

            if (playerCameraView == null)
                throw new ArgumentNullException(nameof(playerCameraView));

            PlayerCameraPresenter playerCameraPresenter =
                _playerCameraPresenterFactory.Create(playerCamera, playerCameraView);
            playerCameraView.Construct(playerCameraPresenter);

            return playerCameraView;
        }
    }
}