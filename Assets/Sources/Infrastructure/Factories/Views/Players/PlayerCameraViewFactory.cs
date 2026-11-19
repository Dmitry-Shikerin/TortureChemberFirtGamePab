using System;
using Controllers.Player;
using Domain.Players.PlayerCameras;
using Infrastructure.Factories.Controllers.Players;
using Presentation.Views.Player;
using PresentationInterfaces.Views.Players;

namespace Infrastructure.Factories.Views.Players
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