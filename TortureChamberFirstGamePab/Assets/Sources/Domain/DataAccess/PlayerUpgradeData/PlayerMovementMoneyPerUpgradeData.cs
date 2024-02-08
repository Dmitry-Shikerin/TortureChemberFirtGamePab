using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData
{
    public class PlayerMovementMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeMovement")]
        public int MoneyPerUpgradeMovement { get; set; }
    }
}