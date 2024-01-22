using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData
{
    public class TavernMoodData
    {
        [JsonProperty("moodValue")]
        public float MoodValue { get; set; }
    }
}