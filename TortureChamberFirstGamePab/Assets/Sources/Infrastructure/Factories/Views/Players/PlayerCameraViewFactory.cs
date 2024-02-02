using System;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players.PlayerCameras;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.InfrastructureInterfaces.Factories;
using Sources.Presentation.Views.Player;
using Sources.PresentationInterfaces.Views;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerCameraViewFactory : IFactory<IView>
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