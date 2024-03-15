using System;
using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.Containers.Players;
using Scripts.Domain.Players.PlayerCameras;
using Scripts.Infrastructure.Factories.Views.UI.AudioSources;
using Scripts.InfrastructureInterfaces.Factories.Prefabs;
using Scripts.Presentation.Containers.GamePoints;
using Scripts.Presentation.Views.Player;

namespace Scripts.Infrastructure.Factories.Views.Players
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

            PlayerMovementView playerMovementView = _playerMovementViewFactory.Create(
                player.Movement, player.Inventory, playerView.Movement, playerView.Animation);

            playerMovementView.SetPosition(_rootGamePoints.PlayerSpawnPoint.transform.position);

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