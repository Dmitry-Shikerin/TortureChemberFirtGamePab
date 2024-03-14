using Newtonsoft.Json;
using Sources.DomainInterfaces.Data;

namespace Sources.Domain.DataAccess.SettingData
{
    public class VolumeData : IDataModel
    {
        [JsonProperty("step")] public int Step { get; set; }
    }
}