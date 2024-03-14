using Newtonsoft.Json;
using Sources.DomainInterfaces.Data;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData
{
    public class PlayerInventoryUpgradeData : IDataModel
    {
        [JsonProperty("currentAmountInventory")]
        public float CurrentAmountInventory { get; set; }

        [JsonProperty("addedAmountInventory")] public float AddedAmountInventory { get; set; }

        [JsonProperty("maximumAmountInventory")]
        public int MaximumLevelInventory { get; set; }

        [JsonProperty("currentLevelInventory")]
        public int CurrentLevelInventory { get; set; }

        [JsonProperty("moneyPerUpgradesInventory")]
        public PlayerInventoryMoneyPerUpgradeData[] MoneyPerUpgradesInventory { get; set; }
    }
}