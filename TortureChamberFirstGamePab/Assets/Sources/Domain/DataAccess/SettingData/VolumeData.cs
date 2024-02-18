using Newtonsoft.Json;

namespace Sources.Domain.DataAccess.SettingData
{
    public class VolumeData
    {
        [JsonProperty("volumeValue")]
        public float VolumeValue { get; set; }
        
        [JsonProperty("step")]
        public int Step { get; set; }
    }
}