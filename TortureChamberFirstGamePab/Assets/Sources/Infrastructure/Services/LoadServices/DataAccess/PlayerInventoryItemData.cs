using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess
{
    public class PlayerInventoryItemData
    {
        [JsonProperty("title")]
       public  string Title { get; set; }
        
        [JsonProperty("price")]
        public int Price { get; set; }
        
        [JsonProperty("waitingTime")]
        public float WaitingTime { get; set; }
    }
}