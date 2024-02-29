using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.Datas.Players;
using Sources.Domain.Players.PlayerCameras;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.UI.AudioSources;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Containers.GamePoints;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Player.Inventory;

namespace Sources.Infrastructure.Factories.Views.Players
{
    public class PlayerViewFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly PlayerMovementViewFactory _playerMovementViewFactory;
        private readonly PlayerWalletViewFactory _playerWalletViewFactory;
        private readonly PlayerCameraViewFactory _playerCameraViewFactory;
        private readonly PlayerInventoryViewFactory _playerInventoryViewFactory;
        private readonly AudioSourceUIFactory _audioSourceUIFactory;
        private readonly RootGamePoints _rootGamePoints;

        public PlayerViewFactory
        (
            IPrefabFactory prefabFactory,
            PlayerMovementViewFactory playerMovementViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory,
            PlayerInventoryViewFactory playerInventoryViewFactory,
            AudioSourceUIFactory audioSourceUIFactory,
            RootGamePoints rootGamePoints
        )
        {
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _playerMovementViewFactory = playerMovementViewFactory ??
                                         throw new ArgumentNullException(nameof(playerMovementViewFactory));
            _playerWalletViewFactory = playerWalletViewFactory ??
                                       throw new ArgumentNullException(nameof(playerWalletViewFactory));
            _playerCameraViewFactory = playerCameraViewFactory ??
                                       throw new ArgumentNullException(nameof(playerCameraViewFactory));
            _playerInventoryViewFactory = playerInventoryViewFactory ??
                                          throw new ArgumentNullException(nameof(playerInventoryViewFactory));
            _audioSourceUIFactory = audioSourceUIFactory ?? 
                                    throw new ArgumentNullException(nameof(audioSourceUIFactory));
            _rootGamePoints = rootGamePoints 
                ? rootGamePoints : throw new ArgumentNullException(nameof(rootGamePoints));
        }

        public PlayerView Create(Player player, PlayerCameraView playerCameraView)
        {
            PlayerView playerView = _prefabFactory.Create<PlayerView>(
                Constant.PrefabPaths.PlayerView);
            
            PlayerMovementView playerMovementView = _playerMovementViewFactory.Create(
                player.Movement, player.Inventory, playerView.Movement, playerView.Animation);
            
            playerMovementView.SetPosition(_rootGamePoints.PlayerSpawnPoint.transform.position);

            PlayerWalletView playerWalletView =
                _playerWalletViewFactory.Create(player.Wallet, playerView.Wallet);

            _audioSourceUIFactory.Create(player.Wallet, playerView.AudioSourcesContainer.Wallet);

            PlayerCamera playerCamera = new PlayerCamera();
            _playerCameraViewFactory.Create(playerCamera, playerCameraView);
            playerCameraView.SetTargetTransform(playerMovementView.Transform);

            PlayerInventoryView playerInventoryView =
                _playerInventoryViewFactory.Create(player.Inventory, playerView.Inventory);

            _audioSourceUIFactory.Create(player.Inventory, playerView.AudioSourcesContainer.Inventory);

            return playerView;
        }
    }
}