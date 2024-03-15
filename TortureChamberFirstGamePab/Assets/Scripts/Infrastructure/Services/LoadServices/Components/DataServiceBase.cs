using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;
using UnityEngine;

namespace Scripts.Infrastructure.Services.LoadServices.Components
{
    public abstract class DataServiceBase
    {
        protected T LoadData<T>(string key)
            where T : IDataModel
        {
            string json = PlayerPrefs.GetString(key, string.Empty);

            return JsonConvert.DeserializeObject<T>(json);
        }

        protected void SaveData<T>(T dataModel, string key)
            where T : IDataModel
        {
            string json = JsonConvert.SerializeObject(dataModel);
            PlayerPrefs.SetString(key, json);
        }
    }
}