using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.Players;
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

        //TODO все методы возращают один и тот же класс, опасно?
        private Upgrader LoadCharismaUpgrader()
        {
            string json = PlayerPrefs.GetString(Constant.UpgradeDataKey.CharismaKey, string.Empty);
            PlayerCharismaUpgradeData charismaData =
                JsonConvert.DeserializeObject<PlayerCharismaUpgradeData>(json);

            //TODO нужен экстеншн
            int[] moneyPerUpgradeCharisma = new int[charismaData.MoneyPerUpgradesCharisma.Length];

            for (int i = 0; i < charismaData.MoneyPerUpgradesCharisma.Length; i++)
            {
                moneyPerUpgradeCharisma[i] = charismaData.MoneyPerUpgradesCharisma[i].MoneyPerUpgradeCharisma;
            }

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

            int[] moneyPerUpgradeInventory = new int[inventoryData.MoneyPerUpgradesInventory.Length];

            for (int i = 0; i < inventoryData.MoneyPerUpgradesInventory.Length; i++)
            {
                moneyPerUpgradeInventory[i] = inventoryData.MoneyPerUpgradesInventory[i].MoneyPerUpgradeInventory;
            }

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

            int[] moneyPerUpgradeMovement = new int[movementData.MoneyPerUpgradesMovement.Length];

            for (int i = 0; i < movementData.MoneyPerUpgradesMovement.Length; i++)
            {
                moneyPerUpgradeMovement[i] = movementData.MoneyPerUpgradesMovement[i].MoneyPerUpgradeMovement;
            }

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
            //TODO правильно ли
            PlayerCharismaMoneyPerUpgradeData[]
                playerCharismaMoneyPerUpgradeData =
                    new PlayerCharismaMoneyPerUpgradeData[charismaUpgrader.MoneyPerUpgrades.Count];

            for (int i = 0; i < charismaUpgrader.MoneyPerUpgrades.Count; i++)
            {
                playerCharismaMoneyPerUpgradeData[i].MoneyPerUpgradeCharisma =
                    charismaUpgrader.MoneyPerUpgrades[i];
            }

            PlayerCharismaUpgradeData playerCharismaUpgradeData = new PlayerCharismaUpgradeData()
            {
                CurrentAmountCharisma = charismaUpgrader.CurrentAmountUpgrade,
                AddedAmountCharisma = charismaUpgrader.AddedAmountUpgrade,
                MaximumAmountCharisma = charismaUpgrader.MaximumUpgradeAmount,
                CurrentLevelCharisma = charismaUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesCharisma = playerCharismaMoneyPerUpgradeData,
            };

            string json = JsonConvert.SerializeObject(playerCharismaUpgradeData);
            PlayerPrefs.SetString(Constant.UpgradeDataKey.CharismaKey, json);
        }

        private void SaveInventory(Upgrader inventoryUpgrader)
        {
            PlayerInventoryMoneyPerUpgradeData[]
                playerInventoryMoneyPerUpgradeData =
                    new PlayerInventoryMoneyPerUpgradeData[inventoryUpgrader.MoneyPerUpgrades.Count];

            for (int i = 0; i < inventoryUpgrader.MoneyPerUpgrades.Count; i++)
            {
                playerInventoryMoneyPerUpgradeData[i].MoneyPerUpgradeInventory =
                    inventoryUpgrader.MoneyPerUpgrades[i];
            }

            PlayerInventoryUpgradeData playerCharismaUpgradeData = new PlayerInventoryUpgradeData()
            {
                CurrentAmountInventory = inventoryUpgrader.CurrentAmountUpgrade,
                AddedAmountInventory = inventoryUpgrader.AddedAmountUpgrade,
                MaximumAmountInventory = inventoryUpgrader.MaximumUpgradeAmount,
                CurrentLevelInventory = inventoryUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesInventory = playerInventoryMoneyPerUpgradeData,
            };

            string json = JsonConvert.SerializeObject(playerCharismaUpgradeData);
            PlayerPrefs.SetString(Constant.UpgradeDataKey.InventoryKey, json);
        }

        private void SaveMovement(Upgrader movementUpgrader)
        {
            PlayerMovementMoneyPerUpgradeData[] playerMovementMoneyPerUpgradeData =
                new PlayerMovementMoneyPerUpgradeData[movementUpgrader.MoneyPerUpgrades.Count];

            for (int i = 0; i < movementUpgrader.MoneyPerUpgrades.Count; i++)
            {
                playerMovementMoneyPerUpgradeData[i].MoneyPerUpgradeMovement =
                    movementUpgrader.MoneyPerUpgrades[i];
            }

            PlayerMovementUpgradeData playerMovementUpgradeData = new PlayerMovementUpgradeData()
            {
                CurrentAmountMovement = movementUpgrader.CurrentAmountUpgrade,
                AddedAmountMovement = movementUpgrader.AddedAmountUpgrade,
                MaximumAmountMovement = movementUpgrader.MaximumUpgradeAmount,
                CurrentLevelMovement = movementUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesMovement = playerMovementMoneyPerUpgradeData,
            };

            string json = JsonConvert.SerializeObject(playerMovementUpgradeData);
            PlayerPrefs.SetString(Constant.UpgradeDataKey.MovementKey, json);
        }
    }
}