using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sources.Domain.DataModels;
using Sources.InfrastructureInterfaces.DataSources;
using UnityEngine;

namespace Sources.Infrastructure.DataSources
{
    public class PlayerPrefsDataSource : IDataSource
    {
        private const string Key = "SavedModels";

        public IEnumerable<DataModel> Load()
        {
            var json = PlayerPrefs.GetString(Key);
            return JsonConvert.DeserializeObject<CollectionWrapper<DataModel>>(json).Collection;
        }

        public void Save(IEnumerable<DataModel> models)
        {
            PlayerPrefs.SetString(Key,
                JsonConvert.SerializeObject(
                    new CollectionWrapper<DataModel>(models.ToArray())));
        }
    }
}