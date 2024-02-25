using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.Datas.Players;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.DomainInterfaces.Data;
using Sources.Extensions.Domain;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class PlayerDataService : DataServiceBase, IDataService<Player>
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
            Debug.Log("PlayerDataService Clear");
        }

        private PlayerMovement LoadMovement() => 
            new(LoadData<PlayerMovementData>(Constant.DataKey.MovementKey));

        private PlayerInventory LoadInventory()
        {
            string json = PlayerPrefs.GetString(Constant.DataKey.InventoryKey, string.Empty);
            PlayerInventoryData inventoryData = JsonConvert.DeserializeObject<PlayerInventoryData>(json);

            return new PlayerInventory();
        }

        private PlayerWallet LoadWallet() => 
            new(LoadData<PlayerWalletData>(Constant.DataKey.WalletKey));

        private void Save(PlayerMovement movement)
        {
            PlayerMovementData movementData = new PlayerMovementData()
            {
                Position = movement.Position.ToVector3Data(),
                Direction = movement.RotationAngle,
            };

            SaveData(movementData, Constant.DataKey.MovementKey);
        }

        private void Save(PlayerInventory inventory)
        {
        }

        private void Save(PlayerWallet wallet)
        {
            PlayerWalletData playerWalletData = new PlayerWalletData()
            {
                Coins = wallet.Coins.GetValue,
                Score = wallet.Score.GetValue
            };
            
            SaveData(playerWalletData, Constant.DataKey.WalletKey);
        }
    }
}