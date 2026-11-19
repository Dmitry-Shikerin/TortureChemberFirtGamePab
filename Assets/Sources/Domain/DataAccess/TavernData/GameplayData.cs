using DomainInterfaces.Data;
using Newtonsoft.Json;

namespace Domain.DataAccess.TavernData
{
    public class GameplayData : IDataModel
    {
        [JsonProperty("maximumVisitorsCapacity")]
        public int MaximumVisitorsCapacity { get; set; }
    }
}