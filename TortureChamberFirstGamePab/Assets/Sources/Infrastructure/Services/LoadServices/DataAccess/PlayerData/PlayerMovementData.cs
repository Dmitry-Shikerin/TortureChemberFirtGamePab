using Newtonsoft.Json;
using Sources.Domain.Data;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData
{
    public class PlayerMovementData
    {
        [JsonProperty("position")] 
        public Vector3Data Position { get; set; }
        
        [JsonProperty("direction")] 
        public float Direction { get; set; }
    }
}