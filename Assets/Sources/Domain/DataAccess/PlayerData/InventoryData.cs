using Newtonsoft.Json;

namespace Domain.DataAccess.PlayerData
{
    public class InventoryData
    {
        [JsonProperty("items")]
        public InventoryItemData[] Items { get; set; }
    }
}