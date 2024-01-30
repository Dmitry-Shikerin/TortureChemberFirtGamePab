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
        
        public IPlayerCameraView Create(PlayerCamera playerCamera)
        {
            if (playerCamera == null)
                throw new ArgumentNullException(nameof(playerCamera));

            //TODO таким образом мы не получаем вью снаружи и не даем возможность переиспользовать фабрику?
            PlayerCameraView playerCameraView = Object.FindObjectOfType<PlayerCameraView>();
            
            PlayerCameraPresenter playerCameraPresenter =
                _playerCameraPresenterFactory.Create(playerCamera, playerCameraView);
            playerCameraView.Construct(playerCameraPresenter);

            return playerCameraView;
        }
    }
}