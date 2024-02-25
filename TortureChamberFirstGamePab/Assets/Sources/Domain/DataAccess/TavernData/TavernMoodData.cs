using Newtonsoft.Json;
using Sources.DomainInterfaces.Data;

namespace Sources.Domain.DataAccess.TavernData
{
    public class TavernMoodData : IDataModel
    {
        [JsonProperty("moodValue")]
        public float MoodValue { get; set; }
    }
}