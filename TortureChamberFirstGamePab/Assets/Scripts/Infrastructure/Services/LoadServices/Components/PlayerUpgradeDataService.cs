using System.Linq;
using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.Containers.Players;
using Scripts.Domain.DataAccess.PlayerUpgradeData;
using Scripts.Domain.Upgrades;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Scripts.Infrastructure.Services.LoadServices.Components
{
    public class PlayerUpgradeDataService : DataServiceBase, IDataService<PlayerUpgrade>
    {
        public bool CanLoad => PlayerPrefs.HasKey(UpgradeDataKey.CharismaKey);

        public PlayerUpgrade Load() =>
            new (LoadCharismaUpgrader(), LoadInventoryUpgrader(), LoadMovementUpgrader());

        public void Save(PlayerUpgrade @object)
        {
            SaveCharisma(@object.Charisma);
            SaveInventory(@object.Inventory);
            SaveMovement(@object.Movement);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(UpgradeDataKey.CharismaKey);
            PlayerPrefs.DeleteKey(UpgradeDataKey.InventoryKey);
            PlayerPrefs.DeleteKey(UpgradeDataKey.MovementKey);
        }

        private Upgrader LoadCharismaUpgrader()
        {
            CharismaUpgradeData charismaData = LoadData<CharismaUpgradeData>(
                UpgradeDataKey.CharismaKey);

            int[] moneyPerUpgradeCharisma = charismaData.MoneyPerUpgradesCharisma
                .Select(money => money.MoneyPerUpgradeCharisma)
                .ToArray();

            return new Upgrader(
                charismaData.CurrentAmountCharisma,
                charismaData.AddedAmountCharisma,
                charismaData.MaximumLevelCharisma,
                charismaData.CurrentLevelCharisma,
                moneyPerUpgradeCharisma);
        }

        private Upgrader LoadInventoryUpgrader()
        {
            InventoryUpgradeData inventoryData = LoadData<InventoryUpgradeData>(
                UpgradeDataKey.InventoryKey);

            int[] moneyPerUpgradeInventory = inventoryData.MoneyPerUpgradesInventory
                .Select(money => money.MoneyPerUpgradeInventory)
                .ToArray();

            return new Upgrader(
                inventoryData.CurrentAmountInventory,
                inventoryData.AddedAmountInventory,
                inventoryData.MaximumLevelInventory,
                inventoryData.CurrentLevelInventory,
                moneyPerUpgradeInventory);
        }

        private Upgrader LoadMovementUpgrader()
        {
            MovementUpgradeData movementData = LoadData<MovementUpgradeData>(
                UpgradeDataKey.MovementKey);

            int[] moneyPerUpgradeMovement = movementData.MoneyPerUpgradesMovement
                .Select(money => money.MoneyPerUpgradeMovement)
                .ToArray();

            return new Upgrader(
                movementData.CurrentAmountMovement,
                movementData.AddedAmountMovement,
                movementData.MaximumLevelMovement,
                movementData.CurrentLevelMovement,
                moneyPerUpgradeMovement);
        }

        private void SaveCharisma(Upgrader charismaUpgrader)
        {
            CharismaMoneyPerUpgradeData[] playerCharismaMoneyPerUpgradeDatas =
                charismaUpgrader.MoneyPerUpgrades
                    .Select(money => new CharismaMoneyPerUpgradeData { MoneyPerUpgradeCharisma = money, })
                    .ToArray();

            CharismaUpgradeData playerCharismaUpgradeData = new CharismaUpgradeData
            {
                CurrentAmountCharisma = charismaUpgrader.CurrentAmountUpgrade,
                AddedAmountCharisma = charismaUpgrader.AddedAmountUpgrade,
                MaximumLevelCharisma = charismaUpgrader.MaximumLevel,
                CurrentLevelCharisma = charismaUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesCharisma = playerCharismaMoneyPerUpgradeDatas,
            };

            SaveData(playerCharismaUpgradeData, UpgradeDataKey.CharismaKey);
        }

        private void SaveInventory(Upgrader inventoryUpgrader)
        {
            InventoryMoneyPerUpgradeData[] playerInventoryMoneyPerUpgradeDatas =
                inventoryUpgrader.MoneyPerUpgrades
                    .Select(money => new InventoryMoneyPerUpgradeData { MoneyPerUpgradeInventory = money })
                    .ToArray();

            InventoryUpgradeData playerCharismaUpgradeData = new InventoryUpgradeData
            {
                CurrentAmountInventory = inventoryUpgrader.CurrentAmountUpgrade,
                AddedAmountInventory = inventoryUpgrader.AddedAmountUpgrade,
                MaximumLevelInventory = inventoryUpgrader.MaximumLevel,
                CurrentLevelInventory = inventoryUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesInventory = playerInventoryMoneyPerUpgradeDatas,
            };

            SaveData(playerCharismaUpgradeData, UpgradeDataKey.InventoryKey);
        }

        private void SaveMovement(Upgrader movementUpgrader)
        {
            MovementMoneyPerUpgradeData[] playerMovementMoneyPerUpgradeDatas =
                movementUpgrader.MoneyPerUpgrades
                    .Select(money => new MovementMoneyPerUpgradeData { MoneyPerUpgradeMovement = money })
                    .ToArray();

            MovementUpgradeData playerMovementUpgradeData = new MovementUpgradeData
            {
                CurrentAmountMovement = movementUpgrader.CurrentAmountUpgrade,
                AddedAmountMovement = movementUpgrader.AddedAmountUpgrade,
                MaximumLevelMovement = movementUpgrader.MaximumLevel,
                CurrentLevelMovement = movementUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesMovement = playerMovementMoneyPerUpgradeDatas,
            };

            SaveData(playerMovementUpgradeData, UpgradeDataKey.MovementKey);
        }
    }
}