using Newtonsoft.Json;
using Sources.DomainInterfaces.Data;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData
{
    public class PlayerCharismaUpgradeData : IDataModel
    {
        [JsonProperty("currentAmountCharisma")]
        public float CurrentAmountCharisma { get; set; }
        
        [JsonProperty("addedAmountCharisma")]
        public float AddedAmountCharisma { get; set; }
        
        [JsonProperty("maximumAmountCharisma")]
        public int MaximumLevelCharisma { get; set; }
        
        [JsonProperty("currentLevelCharisma")]
        public int CurrentLevelCharisma { get; set; }
        
        [JsonProperty("moneyPerUpgradesCharisma")]
        public PlayerCharismaMoneyPerUpgradeData[] MoneyPerUpgradesCharisma { get; set; }
    }
}