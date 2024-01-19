using Newtonsoft.Json;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Extensions.Domain;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class PlayerDataService : IPlayerDataService
    {
        private const string MovementKey = nameof(PlayerMovement);
        private const string InventoryKey = nameof(PlayerInventory);
        private const string WalletKey = nameof(PlayerWallet);

        //TODO нужен метод для очищения префсов
        public Player LoadPlayer() => 
            new(LoadMovement(), LoadInventory(), LoadWallet());

        public void Save(Player player)
        {
            Debug.Log("Game saved");
            Save(player.Inventory);
            Save(player.Movement);
            Save(player.Wallet);
        }

        //TODO здесь ли должен быть этот метод?
        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }

        public PlayerMovement LoadMovement()
        {
            string json = PlayerPrefs.GetString(MovementKey, string.Empty);
            PlayerMovementData movementData = JsonConvert.DeserializeObject<PlayerMovementData>(json);

            return new PlayerMovement(movementData);
        }

        public PlayerInventory LoadInventory()
        {
            string json = PlayerPrefs.GetString(InventoryKey, string.Empty);
            PlayerInventoryData inventoryData = JsonConvert.DeserializeObject<PlayerInventoryData>(json);

            //TODO чтобы засунуть айтемы в плейер инсвентори нужна фабрика
            return new PlayerInventory();
        }

        public PlayerWallet LoadWallet()
        {
            string json = PlayerPrefs.GetString(WalletKey, string.Empty);
            PlayerWalletData walletData = JsonConvert.DeserializeObject<PlayerWalletData>(json);

            return new PlayerWallet(walletData);
        }

        public void Save(PlayerMovement movement)
        {
            PlayerMovementData movementData = new PlayerMovementData()
            {
                Position = movement.Position.Vector3ToVector3Data(),
            };

            string json = JsonConvert.SerializeObject(movementData);
            PlayerPrefs.SetString(MovementKey, json);
        }

        public void Save(PlayerInventory inventory)
        {
            
        }

        public void Save(PlayerWallet wallet)
        {
            PlayerWalletData walletData = new PlayerWalletData()
            {
                Coins = wallet.Coins.GetValue,
            };
            
            string json = JsonConvert.SerializeObject(walletData);
            PlayerPrefs.SetString(WalletKey, json);
        }
    }
}