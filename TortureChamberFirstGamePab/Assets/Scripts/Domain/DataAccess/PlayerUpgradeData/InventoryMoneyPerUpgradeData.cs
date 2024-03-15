using Newtonsoft.Json;

namespace Scripts.Domain.DataAccess.PlayerUpgradeData
{
    public class InventoryMoneyPerUpgradeData
    {
        [JsonProperty("moneyPerUpgradeInventory")]
        public int MoneyPerUpgradeInventory { get; set; }
    }
}