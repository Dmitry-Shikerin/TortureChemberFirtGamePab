using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData
{
    public class PlayerInventoryData
    {
        [JsonProperty("items")]
        public PlayerInventoryItemData[] Items { get; set; }
    }
}