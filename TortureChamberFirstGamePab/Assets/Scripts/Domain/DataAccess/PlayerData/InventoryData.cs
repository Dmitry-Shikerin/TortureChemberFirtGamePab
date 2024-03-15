using Newtonsoft.Json;

namespace Scripts.Domain.DataAccess.PlayerData
{
    public class InventoryData
    {
        [JsonProperty("items")]
        public InventoryItemData[] Items { get; set; }
    }
}