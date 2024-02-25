using Newtonsoft.Json;
using Sources.DomainInterfaces.Data;

namespace Sources.Domain.DataAccess.TavernData
{
    public class GameplayData : IDataModel
    {
        [JsonProperty("maximumVisitorsCapacity")]
        public int MaximumVisitorsCapacity { get; set; }
    }
}