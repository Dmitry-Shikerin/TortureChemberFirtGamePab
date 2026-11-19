using DomainInterfaces.Data;
using Newtonsoft.Json;

namespace Domain.DataAccess.SettingData
{
    public class TutorialData : IDataModel
    {
        [JsonProperty("hasCompleted")]
        public bool HasCompleted { get; set; }
    }
}