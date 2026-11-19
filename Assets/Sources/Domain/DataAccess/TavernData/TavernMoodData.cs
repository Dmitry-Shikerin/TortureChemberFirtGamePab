using DomainInterfaces.Data;
using Newtonsoft.Json;

namespace Domain.DataAccess.TavernData
{
    public class TavernMoodData : IDataModel
    {
        [JsonProperty("moodValue")] public float MoodValue { get; set; }
    }
}