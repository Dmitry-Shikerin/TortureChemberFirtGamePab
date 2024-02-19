using System;
using Newtonsoft.Json;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.DataAccess.SettingData;
using Sources.Domain.Settings;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class SettingDataService : IDataService<Setting>
    {
        private readonly Setting _setting;

        public SettingDataService(Setting setting)
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
        }

        public bool CanLoad => PlayerPrefs.HasKey(Constant.SettingDataKey.VolumeKey);

        public Setting Load()
        {
            if (CanLoad == false)
                return null;

            var volume = LoadVolume();
            _setting.Volume.Step = volume.Step;

            return null;
        }

        public void Save(Setting @object)
        {
            SaveVolume(@object.Volume);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(Constant.SettingDataKey.VolumeKey);
            Debug.Log("SettingDataService Clear");
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
                Step = volume.Step
            };

            string json = JsonConvert.SerializeObject(volumeData);
            PlayerPrefs.SetString(Constant.SettingDataKey.VolumeKey, json);
        }
    }
}