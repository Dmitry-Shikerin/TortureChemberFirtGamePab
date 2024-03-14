using System.Linq;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Players;
using Sources.Domain.DataAccess.PlayerUpgradeData;
using Sources.Domain.Upgrades;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class PlayerUpgradeDataService : DataServiceBase, IDataService<PlayerUpgrade>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.UpgradeDataKey.CharismaKey);

        public PlayerUpgrade Load()
        {
            return new PlayerUpgrade(LoadCharismaUpgrader(), LoadInventoryUpgrader(), LoadMovementUpgrader());
        }

        public void Save(PlayerUpgrade @object)
        {
            SaveCharisma(@object.Charisma);
            SaveInventory(@object.Inventory);
            SaveMovement(@object.Movement);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(Constant.UpgradeDataKey.CharismaKey);
            PlayerPrefs.DeleteKey(Constant.UpgradeDataKey.InventoryKey);
            PlayerPrefs.DeleteKey(Constant.UpgradeDataKey.MovementKey);
        }

        private Upgrader LoadCharismaUpgrader()
        {
            var charismaData = LoadData<PlayerCharismaUpgradeData>(
                Constant.UpgradeDataKey.CharismaKey);

            var moneyPerUpgradeCharisma = charismaData.MoneyPerUpgradesCharisma
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
            var inventoryData = LoadData<PlayerInventoryUpgradeData>(
                Constant.UpgradeDataKey.InventoryKey);

            var moneyPerUpgradeInventory = inventoryData.MoneyPerUpgradesInventory
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
            var movementData = LoadData<PlayerMovementUpgradeData>(
                Constant.UpgradeDataKey.MovementKey);

            var moneyPerUpgradeMovement = movementData.MoneyPerUpgradesMovement
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
            var playerCharismaMoneyPerUpgradeDatas =
                charismaUpgrader.MoneyPerUpgrades
                    .Select(money => new PlayerCharismaMoneyPerUpgradeData { MoneyPerUpgradeCharisma = money })
                    .ToArray();

            var playerCharismaUpgradeData = new PlayerCharismaUpgradeData
            {
                CurrentAmountCharisma = charismaUpgrader.CurrentAmountUpgrade,
                AddedAmountCharisma = charismaUpgrader.AddedAmountUpgrade,
                MaximumLevelCharisma = charismaUpgrader.MaximumLevel,
                CurrentLevelCharisma = charismaUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesCharisma = playerCharismaMoneyPerUpgradeDatas
            };

            SaveData(playerCharismaUpgradeData, Constant.UpgradeDataKey.CharismaKey);
        }

        private void SaveInventory(Upgrader inventoryUpgrader)
        {
            var playerInventoryMoneyPerUpgradeDatas =
                inventoryUpgrader.MoneyPerUpgrades
                    .Select(money => new PlayerInventoryMoneyPerUpgradeData { MoneyPerUpgradeInventory = money })
                    .ToArray();

            var playerCharismaUpgradeData = new PlayerInventoryUpgradeData
            {
                CurrentAmountInventory = inventoryUpgrader.CurrentAmountUpgrade,
                AddedAmountInventory = inventoryUpgrader.AddedAmountUpgrade,
                MaximumLevelInventory = inventoryUpgrader.MaximumLevel,
                CurrentLevelInventory = inventoryUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesInventory = playerInventoryMoneyPerUpgradeDatas
            };

            SaveData(playerCharismaUpgradeData, Constant.UpgradeDataKey.InventoryKey);
        }

        private void SaveMovement(Upgrader movementUpgrader)
        {
            var playerMovementMoneyPerUpgradeDatas =
                movementUpgrader.MoneyPerUpgrades
                    .Select(money => new PlayerMovementMoneyPerUpgradeData { MoneyPerUpgradeMovement = money })
                    .ToArray();

            var playerMovementUpgradeData = new PlayerMovementUpgradeData
            {
                CurrentAmountMovement = movementUpgrader.CurrentAmountUpgrade,
                AddedAmountMovement = movementUpgrader.AddedAmountUpgrade,
                MaximumLevelMovement = movementUpgrader.MaximumLevel,
                CurrentLevelMovement = movementUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesMovement = playerMovementMoneyPerUpgradeDatas
            };

            SaveData(playerMovementUpgradeData, Constant.UpgradeDataKey.MovementKey);
        }
    }
}