using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players.PlayerCameras;

namespace MyProject.Sources.Infrastructure.Factorys.Views
{
    public class PlayerCameraViewFactory
    {
        private readonly PlayerCameraPresenterFactory _playerCameraPresenterFactory;

        public PlayerCameraViewFactory(PlayerCameraPresenterFactory playerCameraPresenterFactory)
        {
            _playerCameraPresenterFactory = playerCameraPresenterFactory ?? 
                                            throw new ArgumentNullException(nameof(playerCameraPresenterFactory));
        }
        
        public IPlayerCameraView Create(PlayerCameraView playerCameraView, PlayerCamera playerCamera)
        {
            if (playerCameraView == null) 
                throw new ArgumentNullException(nameof(playerCameraView));
            if (playerCamera == null)
                throw new ArgumentNullException(nameof(playerCamera));
            
            PlayerCameraPresenter playerCameraPresenter =
                _playerCameraPresenterFactory.Create(playerCamera, playerCameraView);
            playerCameraView.Construct(playerCameraPresenter);

            return playerCameraView;
        }
    }
}