using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.Players.Data;
using Sources.Domain.Upgrades;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class PlayerUpgradeDataService : IDataService<PlayerUpgrade>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.UpgradeDataKey.CharismaKey);

        public PlayerUpgrade Load() => 
            new(LoadCharismaUpgrader(), LoadInventoryUpgrader(), LoadMovementUpgrader());

        public void Save(PlayerUpgrade @object)
        {
            SaveCharisma(@object.CharismaUpgrader);
            SaveInventory(@object.InventoryUpgrader);
            SaveMovement(@object.MovementUpgrader);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }

        private Upgrader LoadCharismaUpgrader()
        {
            string json = PlayerPrefs.GetString(Constant.UpgradeDataKey.CharismaKey, string.Empty);
            PlayerCharismaUpgradeData charismaData =
                JsonConvert.DeserializeObject<PlayerCharismaUpgradeData>(json);

            int[] moneyPerUpgradeCharisma = charismaData.MoneyPerUpgradesCharisma
                .Select(money => money.MoneyPerUpgradeCharisma)
                .ToArray();

            return new Upgrader
            (
                charismaData.CurrentAmountCharisma,
                charismaData.AddedAmountCharisma,
                charismaData.MaximumAmountCharisma,
                charismaData.CurrentLevelCharisma,
                moneyPerUpgradeCharisma
            );
        }

        private Upgrader LoadInventoryUpgrader()
        {
            string json = PlayerPrefs.GetString(Constant.UpgradeDataKey.InventoryKey, string.Empty);
            PlayerInventoryUpgradeData inventoryData =
                JsonConvert.DeserializeObject<PlayerInventoryUpgradeData>(json);
            
            int[] moneyPerUpgradeInventory = inventoryData.MoneyPerUpgradesInventory
                .Select(money => money.MoneyPerUpgradeInventory)
                .ToArray();

            return new Upgrader
            (
                inventoryData.CurrentAmountInventory,
                inventoryData.AddedAmountInventory,
                inventoryData.MaximumAmountInventory,
                inventoryData.CurrentLevelInventory,
                moneyPerUpgradeInventory
            );
        }

        private Upgrader LoadMovementUpgrader()
        {
            string json = PlayerPrefs.GetString(Constant.UpgradeDataKey.MovementKey, string.Empty);
            PlayerMovementUpgradeData movementData =
                JsonConvert.DeserializeObject<PlayerMovementUpgradeData>(json);
            
            int[] moneyPerUpgradeMovement = movementData.MoneyPerUpgradesMovement
                .Select(money => money.MoneyPerUpgradeMovement)
                .ToArray();

            return new Upgrader
            (
                movementData.CurrentAmountMovement,
                movementData.AddedAmountMovement,
                movementData.MaximumAmountMovement,
                movementData.CurrentLevelMovement,
                moneyPerUpgradeMovement
            );
        }

        private void SaveCharisma(Upgrader charismaUpgrader)
        {
            PlayerCharismaMoneyPerUpgradeData[] playerCharismaMoneyPerUpgradeDatas =
                charismaUpgrader.MoneyPerUpgrades
                    .Select(money => new PlayerCharismaMoneyPerUpgradeData()
                        { MoneyPerUpgradeCharisma = money })
                    .ToArray();
            
            PlayerCharismaUpgradeData playerCharismaUpgradeData = new PlayerCharismaUpgradeData()
            {
                CurrentAmountCharisma = charismaUpgrader.CurrentAmountUpgrade,
                AddedAmountCharisma = charismaUpgrader.AddedAmountUpgrade,
                MaximumAmountCharisma = charismaUpgrader.MaximumUpgradeAmount,
                CurrentLevelCharisma = charismaUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesCharisma = playerCharismaMoneyPerUpgradeDatas,
            };

            string json = JsonConvert.SerializeObject(playerCharismaUpgradeData);
            PlayerPrefs.SetString(Constant.UpgradeDataKey.CharismaKey, json);
        }

        private void SaveInventory(Upgrader inventoryUpgrader)
        {
            PlayerInventoryMoneyPerUpgradeData[] playerInventoryMoneyPerUpgradeDatas =
                inventoryUpgrader.MoneyPerUpgrades
                    .Select(money => new PlayerInventoryMoneyPerUpgradeData() 
                        { MoneyPerUpgradeInventory = money })
                    .ToArray();

            PlayerInventoryUpgradeData playerCharismaUpgradeData = new PlayerInventoryUpgradeData()
            {
                CurrentAmountInventory = inventoryUpgrader.CurrentAmountUpgrade,
                AddedAmountInventory = inventoryUpgrader.AddedAmountUpgrade,
                MaximumAmountInventory = inventoryUpgrader.MaximumUpgradeAmount,
                CurrentLevelInventory = inventoryUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesInventory = playerInventoryMoneyPerUpgradeDatas,
            };

            string json = JsonConvert.SerializeObject(playerCharismaUpgradeData);
            PlayerPrefs.SetString(Constant.UpgradeDataKey.InventoryKey, json);
        }

        private void SaveMovement(Upgrader movementUpgrader)
        {
            PlayerMovementMoneyPerUpgradeData[] playerMovementMoneyPerUpgradeDatas =
                movementUpgrader.MoneyPerUpgrades
                .Select(money => new PlayerMovementMoneyPerUpgradeData() 
                    { MoneyPerUpgradeMovement = money })
                .ToArray();
            
            PlayerMovementUpgradeData playerMovementUpgradeData = new PlayerMovementUpgradeData()
            {
                CurrentAmountMovement = movementUpgrader.CurrentAmountUpgrade,
                AddedAmountMovement = movementUpgrader.AddedAmountUpgrade,
                MaximumAmountMovement = movementUpgrader.MaximumUpgradeAmount,
                CurrentLevelMovement = movementUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesMovement = playerMovementMoneyPerUpgradeDatas,
            };

            string json = JsonConvert.SerializeObject(playerMovementUpgradeData);
            PlayerPrefs.SetString(Constant.UpgradeDataKey.MovementKey, json);
        }
    }
}