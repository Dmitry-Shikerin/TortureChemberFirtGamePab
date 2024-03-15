using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.PlayerUpgradeData
{
    public class CharismaUpgradeData : IDataModel
    {
        [JsonProperty("currentAmountCharisma")]
        public float CurrentAmountCharisma { get; set; }

        [JsonProperty("addedAmountCharisma")] public float AddedAmountCharisma { get; set; }

        [JsonProperty("maximumAmountCharisma")]
        public int MaximumLevelCharisma { get; set; }

        [JsonProperty("currentLevelCharisma")] public int CurrentLevelCharisma { get; set; }

        [JsonProperty("moneyPerUpgradesCharisma")]
        public CharismaMoneyPerUpgradeData[] MoneyPerUpgradesCharisma { get; set; }
    }
}