using System;
using Domain.Constants;
using Domain.DataAccess.Containers.Settings;
using Domain.DataAccess.SettingData;
using Domain.Settings;
using InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Infrastructure.Services.LoadServices.Components
{
    public class SettingDataService : DataServiceBase, IDataService<Setting>
    {
        private readonly Setting _setting;

        public SettingDataService(Setting setting)
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
        }

        public bool CanLoad =>
            CanLoadData<VolumeData>(SettingDataKey.VolumeKey);

        public Setting Load()
        {
            if (TryLoadData(SettingDataKey.VolumeKey, out VolumeData volumeData))
            {
                _setting.Volume.Step = volumeData.Step;
            }

            if (TryLoadData(SettingDataKey.TutorialKey, out TutorialData tutorialData))
            {
                _setting.Tutorial.HasCompleted = tutorialData.HasCompleted;
            }

            return _setting;
        }

        public void Save(Setting @object)
        {
            SaveVolume(@object.Volume);
            SaveTutorial(@object.Tutorial);
            PlayerPrefs.Save();
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(SettingDataKey.VolumeKey);
            PlayerPrefs.DeleteKey(SettingDataKey.TutorialKey);
            PlayerPrefs.Save();
        }

        private void SaveVolume(Volume volume)
        {
            VolumeData volumeData = new VolumeData
            {
                Step = volume.Step,
            };

            SaveData(volumeData, SettingDataKey.VolumeKey);
        }

        private void SaveTutorial(Tutorial tutorial)
        {
            TutorialData tutorialData = new TutorialData
            {
                HasCompleted = tutorial.HasCompleted,
            };

            SaveData(tutorialData, SettingDataKey.TutorialKey);
        }
    }
}