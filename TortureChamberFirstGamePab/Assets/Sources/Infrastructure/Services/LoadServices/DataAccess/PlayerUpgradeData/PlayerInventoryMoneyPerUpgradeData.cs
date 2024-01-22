using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerUpgradeData
{
    public class PlayerInventoryMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeInventory")]
        public int MoneyPerUpgradeInventory { get; set; }
    }
}