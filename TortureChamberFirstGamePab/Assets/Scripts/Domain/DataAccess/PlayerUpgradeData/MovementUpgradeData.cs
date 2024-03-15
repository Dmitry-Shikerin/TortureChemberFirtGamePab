using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.PlayerUpgradeData
{
    public class MovementUpgradeData : IDataModel
    {
        [JsonProperty("currentAmountMovement")]
        public float CurrentAmountMovement { get; set; }

        [JsonProperty("addedAmountMovement")] public float AddedAmountMovement { get; set; }

        [JsonProperty("maximumAmountMovement")]
        public int MaximumLevelMovement { get; set; }

        [JsonProperty("currentLevelMovement")] public int CurrentLevelMovement { get; set; }

        [JsonProperty("moneyPerUpgradesMovement")]
        public MovementMoneyPerUpgradeData[] MoneyPerUpgradesMovement { get; set; }
    }
}