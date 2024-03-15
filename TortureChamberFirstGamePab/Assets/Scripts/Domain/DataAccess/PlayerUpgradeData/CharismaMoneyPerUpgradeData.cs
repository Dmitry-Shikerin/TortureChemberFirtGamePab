using Newtonsoft.Json;

namespace Scripts.Domain.DataAccess.PlayerUpgradeData
{
    public class CharismaMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeCharisma")]
        public int MoneyPerUpgradeCharisma { get; set; }
    }
}