﻿using Newtonsoft.Json;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Extensions.Domain;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    //TODO исправить дубляж в этих классах
    public class PlayerDataService : IDataService<Player>
    {
        private const string MovementKey = nameof(PlayerMovement);
        private const string InventoryKey = nameof(PlayerInventory);
        private const string WalletKey = nameof(PlayerWallet);

        public bool CanLoad => PlayerPrefs.HasKey(MovementKey);

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
            PlayerPrefs.DeleteAll();
        }

        private PlayerMovement LoadMovement()
        {
            string json = PlayerPrefs.GetString(MovementKey, string.Empty);
            PlayerMovementData movementData = JsonConvert.DeserializeObject<PlayerMovementData>(json);

            return new PlayerMovement(movementData);
        }

        private PlayerInventory LoadInventory()
        {
            string json = PlayerPrefs.GetString(InventoryKey, string.Empty);
            PlayerInventoryData inventoryData = JsonConvert.DeserializeObject<PlayerInventoryData>(json);

            //TODO чтобы засунуть айтемы в плейер инсвентори нужна фабрика
            return new PlayerInventory();
        }

        private PlayerWallet LoadWallet()
        {
            string json = PlayerPrefs.GetString(WalletKey, string.Empty);
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
            PlayerPrefs.SetString(MovementKey, json);
        }

        private void Save(PlayerInventory inventory)
        {
            
        }

        private void Save(PlayerWallet wallet)
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