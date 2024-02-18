using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.DataAccess.SettingData;
using Sources.Domain.Players.PlayerMovements;
using Sources.Domain.Settings;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class SettingDataService : IDataService<Setting>
    {
        public bool CanLoad => PlayerPrefs.HasKey(Constant.SettingDataKey.VolumeKey);
        public Setting Load()
        {
            return new Setting(LoadVolume());
        }

        public void Save(Setting @object)
        {
            SaveVolume(@object.Volume);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(Constant.SettingDataKey.VolumeKey);
        }

        private Volume LoadVolume()
        {
            string json = PlayerPrefs.GetString(Constant.SettingDataKey.VolumeKey, string.Empty);
            VolumeData volumeData = JsonConvert.DeserializeObject<VolumeData>(json);

            return new Volume(volumeData);
        }

        private void SaveVolume(Volume volume)
        {
            VolumeData volumeData = new VolumeData()
            {
                VolumeValue = volume.VolumeValue,
                Step = volume.Step
            };

            string json = JsonConvert.SerializeObject(volumeData);
            PlayerPrefs.SetString(Constant.SettingDataKey.VolumeKey, json);
        }
    }
}