using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.Containers.Players;
using Scripts.Domain.DataAccess.PlayerData;
using Scripts.Domain.Players;
using Scripts.Domain.Players.PlayerMovements;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using Scripts.Utils.Extensions.Domain;
using UnityEngine;

namespace Scripts.Infrastructure.Services.LoadServices.Components
{
    public class PlayerDataService : DataServiceBase, IDataService<Player>
    {
        public bool CanLoad => PlayerPrefs.HasKey(DataKey.MovementKey);

        public Player Load() =>
            new (LoadMovement(), new PlayerInventory(), LoadWallet());

        public void Save(Player player)
        {
            Save(player.Movement);
            Save(player.Wallet);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(DataKey.MovementKey);
            PlayerPrefs.DeleteKey(DataKey.WalletKey);
        }

        private PlayerMovement LoadMovement() =>
            new (LoadData<MovementData>(DataKey.MovementKey));

        private PlayerWallet LoadWallet() =>
            new (LoadData<WalletData>(DataKey.WalletKey));

        private void Save(PlayerMovement movement)
        {
            MovementData movementData = new MovementData
            {
                Position = movement.Position.ToVector3Data(),
                Direction = movement.RotationAngle,
            };

            SaveData(movementData, DataKey.MovementKey);
        }

        private void Save(PlayerWallet wallet)
        {
            WalletData walletData = new WalletData
            {
                Coins = wallet.Coins.GetValue,
                Score = wallet.Score.GetValue,
            };

            SaveData(walletData, DataKey.WalletKey);
        }
    }
}