using Domain.Data;
using DomainInterfaces.Data;
using Newtonsoft.Json;

namespace Domain.DataAccess.PlayerData
{
    public class MovementData : IDataModel
    {
        [JsonProperty("position")]
        public Vector3Data Position { get; set; }

        [JsonProperty("direction")]
        public float Direction { get; set; }
    }
}