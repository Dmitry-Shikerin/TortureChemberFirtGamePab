using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData
{
    public class PlayerCharismaUpgradeData
    {
        [JsonProperty("currentAmountCharisma")]
        public float CurrentAmountCharisma { get; set; }
        
        [JsonProperty("addedAmountCharisma")]
        public float AddedAmountCharisma { get; set; }
        
        [JsonProperty("maximumAmountCharisma")]
        public float MaximumAmountCharisma { get; set; }
        
        [JsonProperty("currentLevelCharisma")]
        public int CurrentLevelCharisma { get; set; }
        
        [JsonProperty("moneyPerUpgradesCharisma")]
        public PlayerCharismaMoneyPerUpgradeData[] MoneyPerUpgradesCharisma { get; set; }
    }
}