using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData
{
    public class PlayerMovementUpgradeData
    {
        [JsonProperty("currentAmountMovement")]
        public float CurrentAmountMovement { get; set; }
        
        [JsonProperty("addedAmountMovement")]
        public float AddedAmountMovement { get; set; }
        
        [JsonProperty("maximumAmountMovement")]
        public int MaximumLevelMovement { get; set; }
        [JsonProperty("currentLevelMovement")]
        public int CurrentLevelMovement { get; set; }

        
        [JsonProperty("moneyPerUpgradesMovement")]
        public PlayerMovementMoneyPerUpgradeData[] MoneyPerUpgradesMovement { get; set; }
    }
}