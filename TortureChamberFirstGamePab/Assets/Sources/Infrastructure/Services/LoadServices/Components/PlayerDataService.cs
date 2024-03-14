using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Extensions.Domain;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class PlayerDataService : DataServiceBase, IDataService<Player>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.DataKey.MovementKey);

        public Player Load()
        {
            return new Player(LoadMovement(), new PlayerInventory(), LoadWallet());
        }

        public void Save(Player player)
        {
            Save(player.Movement);
            Save(player.Wallet);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(Constant.DataKey.MovementKey);
            PlayerPrefs.DeleteKey(Constant.DataKey.WalletKey);
        }

        private PlayerMovement LoadMovement()
        {
            return new PlayerMovement(LoadData<PlayerMovementData>(Constant.DataKey.MovementKey));
        }

        private PlayerWallet LoadWallet()
        {
            return new PlayerWallet(LoadData<PlayerWalletData>(Constant.DataKey.WalletKey));
        }

        private void Save(PlayerMovement movement)
        {
            var movementData = new PlayerMovementData
            {
                Position = movement.Position.ToVector3Data(),
                Direction = movement.RotationAngle
            };

            SaveData(movementData, Constant.DataKey.MovementKey);
        }

        private void Save(PlayerWallet wallet)
        {
            var playerWalletData = new PlayerWalletData
            {
                Coins = wallet.Coins.GetValue,
                Score = wallet.Score.GetValue
            };

            SaveData(playerWalletData, Constant.DataKey.WalletKey);
        }
    }
}