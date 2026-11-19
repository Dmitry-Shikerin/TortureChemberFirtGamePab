using DomainInterfaces.Data;
using Newtonsoft.Json;

namespace Domain.DataAccess.SettingData
{
    public class VolumeData : IDataModel
    {
        [JsonProperty("step")] 
        public int Step { get; set; }
    }
}