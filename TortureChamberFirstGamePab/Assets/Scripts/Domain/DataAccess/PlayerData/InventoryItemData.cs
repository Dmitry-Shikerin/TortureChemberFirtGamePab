using Newtonsoft.Json;

namespace Scripts.Domain.DataAccess.PlayerData
{
    public class InventoryItemData
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("waitingTime")]
        public float WaitingTime { get; set; }
    }
}