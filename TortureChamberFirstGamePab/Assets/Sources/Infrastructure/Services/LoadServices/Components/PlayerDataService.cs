using Agava.YandexGames;
using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Players;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Extensions.Domain;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class PlayerDataService : IDataService<Player>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.DataKey.MovementKey);

        public Player Load() =>
            new(LoadMovement(), LoadInventory(), LoadWallet());

        public void Save(Player player)
        {
            Debug.Log("Game saved");
            Save(player.Inventory);
            Save(player.Movement);
            Save(player.Wallet);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(Constant.DataKey.MovementKey);
            PlayerPrefs.DeleteKey(Constant.DataKey.InventoryKey);
            PlayerPrefs.DeleteKey(Constant.DataKey.WalletKey);
        }

        private PlayerMovement LoadMovement()
        {
            string json = PlayerPrefs.GetString(Constant.DataKey.MovementKey, string.Empty);
            PlayerMovementData movementData = JsonConvert.DeserializeObject<PlayerMovementData>(json);

            return new PlayerMovement(movementData);
        }

        private PlayerInventory LoadInventory()
        {
            string json = PlayerPrefs.GetString(Constant.DataKey.InventoryKey, string.Empty);
            PlayerInventoryData inventoryData = JsonConvert.DeserializeObject<PlayerInventoryData>(json);

            return new PlayerInventory();
        }

        private PlayerWallet LoadWallet()
        {
            string json = PlayerPrefs.GetString(Constant.DataKey.WalletKey, string.Empty);
            PlayerWalletData walletData = JsonConvert.DeserializeObject<PlayerWalletData>(json);

            return new PlayerWallet(walletData);
        }

        private void Save(PlayerMovement movement)
        {
            PlayerMovementData movementData = new PlayerMovementData()
            {
                Position = movement.Position.ToVector3Data(),
                Direction = movement.RotationAngle,
            };

            string json = JsonConvert.SerializeObject(movementData);
            PlayerPrefs.SetString(Constant.DataKey.MovementKey, json);
        }

        private void Save(PlayerInventory inventory)
        {
        }

        private void Save(PlayerWallet wallet)
        {
            string json = JsonConvert.SerializeObject(
                new PlayerWalletData()
                {
                    Coins = wallet.Coins.GetValue,
                    Score = wallet.Score
                });
            PlayerPrefs.SetString(Constant.DataKey.WalletKey, json);
        }
    }
}