using Newtonsoft.Json;

namespace Sources.Domain.DataAccess.SettingData
{
    public class TutorialData
    {
        [JsonProperty("hasCompleted")]
        public bool HasCompleted { get; set; }
    }
}