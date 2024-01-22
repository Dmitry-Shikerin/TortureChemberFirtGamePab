using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData
{
    public class PlayerCharismaMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeCharisma")]
        public int MoneyPerUpgradeCharisma { get; set; }
    }
}