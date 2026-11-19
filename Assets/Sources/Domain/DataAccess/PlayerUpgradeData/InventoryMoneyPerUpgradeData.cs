using Newtonsoft.Json;

namespace Domain.DataAccess.PlayerUpgradeData
{
    public class InventoryMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeInventory")]
        public int MoneyPerUpgradeInventory { get; set; }
    }
}