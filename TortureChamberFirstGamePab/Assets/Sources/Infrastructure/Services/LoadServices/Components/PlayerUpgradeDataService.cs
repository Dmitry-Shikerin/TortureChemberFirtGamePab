using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.Datas.Players;
using Sources.Domain.Upgrades;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class PlayerUpgradeDataService : DataServiceBase, IDataService<PlayerUpgrade>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.UpgradeDataKey.CharismaKey);

        public PlayerUpgrade Load() => 
            new(LoadCharismaUpgrader(), LoadInventoryUpgrader(), LoadMovementUpgrader());

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
            Debug.Log("PlayerUpgradeDataService Clear");
        }

        private Upgrader LoadCharismaUpgrader()
        {
            PlayerCharismaUpgradeData charismaData = LoadData<PlayerCharismaUpgradeData>(
                Constant.UpgradeDataKey.CharismaKey);

            int[] moneyPerUpgradeCharisma = charismaData.MoneyPerUpgradesCharisma
                .Select(money => money.MoneyPerUpgradeCharisma)
                .ToArray();

            return new Upgrader
            (
                charismaData.CurrentAmountCharisma,
                charismaData.AddedAmountCharisma,
                charismaData.MaximumLevelCharisma,
                charismaData.CurrentLevelCharisma,
                moneyPerUpgradeCharisma
            );
        }

        private Upgrader LoadInventoryUpgrader()
        {
            PlayerInventoryUpgradeData inventoryData = LoadData<PlayerInventoryUpgradeData>(
                Constant.UpgradeDataKey.InventoryKey);
            
            int[] moneyPerUpgradeInventory = inventoryData.MoneyPerUpgradesInventory
                .Select(money => money.MoneyPerUpgradeInventory)
                .ToArray();

            return new Upgrader
            (
                inventoryData.CurrentAmountInventory,
                inventoryData.AddedAmountInventory,
                inventoryData.MaximumLevelInventory,
                inventoryData.CurrentLevelInventory,
                moneyPerUpgradeInventory
            );
        }

        private Upgrader LoadMovementUpgrader()
        {
            PlayerMovementUpgradeData movementData = LoadData<PlayerMovementUpgradeData>(
                Constant.UpgradeDataKey.MovementKey);
            
            int[] moneyPerUpgradeMovement = movementData.MoneyPerUpgradesMovement
                .Select(money => money.MoneyPerUpgradeMovement)
                .ToArray();

            return new Upgrader
            (
                movementData.CurrentAmountMovement,
                movementData.AddedAmountMovement,
                movementData.MaximumLevelMovement,
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
                MaximumLevelCharisma = charismaUpgrader.MaximumLevel,
                CurrentLevelCharisma = charismaUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesCharisma = playerCharismaMoneyPerUpgradeDatas,
            };

            SaveData(playerCharismaUpgradeData, Constant.UpgradeDataKey.CharismaKey);
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
                MaximumLevelInventory = inventoryUpgrader.MaximumLevel,
                CurrentLevelInventory = inventoryUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesInventory = playerInventoryMoneyPerUpgradeDatas,
            };

            SaveData(playerCharismaUpgradeData, Constant.UpgradeDataKey.InventoryKey);
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
                MaximumLevelMovement = movementUpgrader.MaximumLevel,
                CurrentLevelMovement = movementUpgrader.CurrentLevelUpgrade.GetValue,
                MoneyPerUpgradesMovement = playerMovementMoneyPerUpgradeDatas,
            };

            SaveData(playerMovementUpgradeData, Constant.UpgradeDataKey.MovementKey);
        }
    }
}