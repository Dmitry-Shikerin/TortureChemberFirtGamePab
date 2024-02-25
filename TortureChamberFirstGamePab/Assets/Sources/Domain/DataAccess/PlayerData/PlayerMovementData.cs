using Newtonsoft.Json;
using Sources.Domain.Data;
using Sources.DomainInterfaces.Data;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData
{
    public class PlayerMovementData : IDataModel
    {
        [JsonProperty("position")] 
        public Vector3Data Position { get; set; }
        
        [JsonProperty("direction")] 
        public float Direction { get; set; }
    }
}