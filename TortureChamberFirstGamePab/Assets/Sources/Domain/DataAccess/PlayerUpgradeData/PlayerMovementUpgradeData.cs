using Newtonsoft.Json;
using Sources.DomainInterfaces.Data;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData;

namespace Sources.Domain.DataAccess.PlayerUpgradeData
{
    public class PlayerMovementUpgradeData : IDataModel
    {
        [JsonProperty("currentAmountMovement")]
        public float CurrentAmountMovement { get; set; }

        [JsonProperty("addedAmountMovement")] public float AddedAmountMovement { get; set; }

        [JsonProperty("maximumAmountMovement")]
        public int MaximumLevelMovement { get; set; }

        [JsonProperty("currentLevelMovement")] public int CurrentLevelMovement { get; set; }

        [JsonProperty("moneyPerUpgradesMovement")]
        public PlayerMovementMoneyPerUpgradeData[] MoneyPerUpgradesMovement { get; set; }
    }
}