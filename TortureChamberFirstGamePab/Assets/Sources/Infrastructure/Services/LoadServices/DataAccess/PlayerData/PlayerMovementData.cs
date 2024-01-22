using Newtonsoft.Json;
using Sources.Domain.Data;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess
{
    public class PlayerMovementData
    {
        [JsonProperty("position")] 
        public Vector3Data Position { get; set; }
        
        [JsonProperty("direction")] 
        public float Direction { get; set; }
    }
}