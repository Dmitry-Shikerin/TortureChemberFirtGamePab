using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.SettingData
{
    public class VolumeData : IDataModel
    {
        [JsonProperty("step")] public int Step { get; set; }
    }
}