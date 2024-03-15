using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.PlayerUpgradeData
{
    public class InventoryUpgradeData : IDataModel
    {
        [JsonProperty("currentAmountInventory")]
        public float CurrentAmountInventory { get; set; }

        [JsonProperty("addedAmountInventory")] public float AddedAmountInventory { get; set; }

        [JsonProperty("maximumAmountInventory")]
        public int MaximumLevelInventory { get; set; }

        [JsonProperty("currentLevelInventory")]
        public int CurrentLevelInventory { get; set; }

        [JsonProperty("moneyPerUpgradesInventory")]
        public InventoryMoneyPerUpgradeData[] MoneyPerUpgradesInventory { get; set; }
    }
}