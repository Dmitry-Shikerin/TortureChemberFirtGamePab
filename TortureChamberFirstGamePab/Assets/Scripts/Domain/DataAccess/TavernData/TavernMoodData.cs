using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.TavernData
{
    public class TavernMoodData : IDataModel
    {
        [JsonProperty("moodValue")] public float MoodValue { get; set; }
    }
}