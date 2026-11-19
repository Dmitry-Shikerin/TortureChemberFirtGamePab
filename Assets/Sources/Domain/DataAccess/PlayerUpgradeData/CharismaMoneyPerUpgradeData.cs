using Newtonsoft.Json;

namespace Domain.DataAccess.PlayerUpgradeData
{
    public class CharismaMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeCharisma")]
        public int MoneyPerUpgradeCharisma { get; set; }
    }
}