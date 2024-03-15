using Newtonsoft.Json;

namespace Scripts.Domain.DataAccess.PlayerUpgradeData
{
    public class MovementMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeMovement")]
        public int MoneyPerUpgradeMovement { get; set; }
    }
}