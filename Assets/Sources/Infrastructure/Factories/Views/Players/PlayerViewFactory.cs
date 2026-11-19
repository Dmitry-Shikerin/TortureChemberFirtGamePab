using System;
using Domain.Constants;
using Domain.DataAccess.Containers.Players;
using Domain.Players.PlayerCameras;
using Infrastructure.Factories.Views.UI.AudioSources;
using InfrastructureInterfaces.Factories.Prefabs;
using Presentation.Containers.GamePoints;
using Presentation.Views.Player;

namespace Infrastructure.Factories.Views.Players
{
    public class PlayerViewFactory
    {
        private readonly AudioSourceUIFactory _audioSourceUIFactory;
        private readonly PlayerCameraViewFactory _playerCameraViewFactory;
        private readonly PlayerInventoryViewFactory _playerInventoryViewFactory;
        private readonly PlayerMovementViewFactory _playerMovementViewFactory;
        private readonly PlayerWalletViewFactory _playerWalletViewFactory;
        private readonly IPrefabFactory _prefabFactory;
        private readonly RootGamePoints _rootGamePoints;

        public PlayerViewFactory(
            IPrefabFactory prefabFactory,
            PlayerMovementViewFactory playerMovementViewFactory,
            PlayerWalletViewFactory playerWalletViewFactory,
            PlayerCameraViewFactory playerCameraViewFactory,
            PlayerInventoryViewFactory playerInventoryViewFactory,
            AudioSourceUIFactory audioSourceUIFactory,
            RootGamePoints rootGamePoints)
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
                ? rootGamePoints
                : throw new ArgumentNullException(nameof(rootGamePoints));
        }

        public PlayerView Create(Player player, PlayerCameraView playerCameraView)
        {
            PlayerView playerView = _prefabFactory.Create<PlayerView>(
                PrefabPath.PlayerView);

            playerView.Movement.SetPosition(player.Movement.HasSavedPosition
                ? player.Movement.Position
                : _rootGamePoints.PlayerSpawnPoint.transform.position);

            PlayerMovementView playerMovementView = _playerMovementViewFactory.Create(
                player.Movement, player.Inventory, playerView.Movement, playerView.Animation);

            _playerWalletViewFactory.Create(player.Wallet, playerView.Wallet);

            _audioSourceUIFactory.Create(player.Wallet, playerView.AudioSourcesContainer.Wallet);

            PlayerCamera playerCamera = new PlayerCamera();
            _playerCameraViewFactory.Create(playerCamera, playerCameraView);
            playerCameraView.SetTargetTransform(playerMovementView.Transform);

            _playerInventoryViewFactory.Create(player.Inventory, playerView.Inventory);

            _audioSourceUIFactory.Create(player.Inventory, playerView.AudioSourcesContainer.Inventory);

            return playerView;
        }
    }
}