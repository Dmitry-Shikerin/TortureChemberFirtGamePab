using System;
using System.Reflection;
using DomainInterfaces.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Infrastructure.Services.LoadServices.Components
{
    public abstract class DataServiceBase
    {
        private static readonly JsonSerializerSettings LoadSettings = new JsonSerializerSettings
        {
            ContractResolver = new SaveDataContractResolver(),
        };

        protected bool CanLoadData<T>(string key)
            where T : class, IDataModel =>
            TryLoadData(key, out T data);

        protected bool TryLoadData<T>(string key, out T data)
            where T : class, IDataModel
        {
            data = null;
            string json = PlayerPrefs.GetString(key, string.Empty);

            if (string.IsNullOrWhiteSpace(json))
            {
                return false;
            }

            try
            {
                JObject document = JObject.Parse(json);

                foreach (JToken token in document.DescendantsAndSelf())
                {
                    if (token.Type == JTokenType.Null)
                    {
                        return false;
                    }
                }

                data = document.ToObject<T>(JsonSerializer.Create(LoadSettings));
                return data != null;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        protected T LoadData<T>(string key)
            where T : class, IDataModel
        {
            if (TryLoadData(key, out T data) == false)
            {
                throw new InvalidOperationException($"Save data is missing or invalid for key '{key}'.");
            }

            return data;
        }

        protected void SaveData<T>(T dataModel, string key)
            where T : IDataModel
        {
            string json = JsonConvert.SerializeObject(dataModel);
            PlayerPrefs.SetString(key, json);
        }

        private sealed class SaveDataContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);
                property.Required = Required.Always;
                return property;
            }
        }
    }
}
