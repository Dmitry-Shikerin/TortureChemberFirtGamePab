using Newtonsoft.Json;
using Scripts.Domain.Data;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.PlayerData
{
    public class MovementData : IDataModel
    {
        [JsonProperty("position")]
        public Vector3Data Position { get; set; }

        [JsonProperty("direction")]
        public float Direction { get; set; }
    }
}