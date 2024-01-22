using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData
{
    public class PlayerInventoryUpgradeData
    {
        [JsonProperty("currentAmountInventory")]
        public float CurrentAmountInventory { get; set; }
        
        [JsonProperty("addedAmountInventory")]
        public float AddedAmountInventory { get; set; }
        
        [JsonProperty("maximumAmountInventory")]
        public float MaximumAmountInventory { get; set; }
        [JsonProperty("currentLevelInventory")]
        public int CurrentLevelInventory { get; set; }

        
        [JsonProperty("moneyPerUpgradesInventory")]
        public PlayerInventoryMoneyPerUpgradeData[] MoneyPerUpgradesInventory { get; set; }
    }
}