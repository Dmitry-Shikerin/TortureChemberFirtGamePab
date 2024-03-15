using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.SettingData
{
    public class TutorialData : IDataModel
    {
        [JsonProperty("hasCompleted")] public bool HasCompleted { get; set; }
    }
}