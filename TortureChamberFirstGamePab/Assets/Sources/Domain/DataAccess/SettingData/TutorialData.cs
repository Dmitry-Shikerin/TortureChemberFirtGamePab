using Newtonsoft.Json;
using Sources.DomainInterfaces.Data;

namespace Sources.Domain.DataAccess.SettingData
{
    public class TutorialData : IDataModel
    {
        [JsonProperty("hasCompleted")]
        public bool HasCompleted { get; set; }
    }
}