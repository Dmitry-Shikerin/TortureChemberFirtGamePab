using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.TavernData
{
    public class GameplayData : IDataModel
    {
        [JsonProperty("maximumVisitorsCapacity")]
        public int MaximumVisitorsCapacity { get; set; }
    }
}