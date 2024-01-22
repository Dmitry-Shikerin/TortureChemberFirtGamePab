using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData
{
    public class GameplayData
    {
        [JsonProperty("maximumVisitorsCapacity")]
        public int MaximumVisitorsCapacity { get; set; }
    }
}