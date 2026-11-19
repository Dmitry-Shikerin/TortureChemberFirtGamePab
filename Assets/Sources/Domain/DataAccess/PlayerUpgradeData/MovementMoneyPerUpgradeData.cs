using Newtonsoft.Json;

namespace Domain.DataAccess.PlayerUpgradeData
{
    public class MovementMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeMovement")]
        public int MoneyPerUpgradeMovement { get; set; }
    }
}