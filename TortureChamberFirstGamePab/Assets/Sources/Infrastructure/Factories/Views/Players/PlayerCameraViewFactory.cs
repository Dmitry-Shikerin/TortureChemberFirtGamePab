using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players.PlayerCameras;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Presentation.Views.Player;

namespace Sources.Infrastructure.Factories.Views.Players
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

            var playerCameraPresenter =
                _playerCameraPresenterFactory.Create(playerCamera, playerCameraView);
            playerCameraView.Construct(playerCameraPresenter);

            return playerCameraView;
        }
    }
}