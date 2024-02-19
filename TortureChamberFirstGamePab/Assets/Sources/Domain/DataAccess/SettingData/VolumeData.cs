using Newtonsoft.Json;

namespace Sources.Domain.DataAccess.SettingData
{
    public class VolumeData
    {
        [JsonProperty("step")]
        public int Step { get; set; }
    }
}