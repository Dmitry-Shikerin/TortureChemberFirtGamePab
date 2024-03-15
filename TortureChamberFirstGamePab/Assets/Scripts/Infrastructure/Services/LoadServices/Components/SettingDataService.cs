using System;
using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.Domain.DataAccess.SettingData;
using Scripts.Domain.Settings;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Scripts.Infrastructure.Services.LoadServices.Components
{
    public class SettingDataService : DataServiceBase, IDataService<Setting>
    {
        private readonly Setting _setting;

        public SettingDataService(Setting setting)
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
        }

        private bool CanLoadTutorial => PlayerPrefs.HasKey(SettingDataKey.TutorialKey);

        public bool CanLoad => PlayerPrefs.HasKey(SettingDataKey.VolumeKey);

        public Setting Load()
        {
            if (CanLoad == false)
                return _setting;

            Volume volume = LoadVolume();
            _setting.Volume.Step = volume.Step;

            if (CanLoadTutorial == false)
                return _setting;

            Tutorial tutorial = LoadTutorial();
            _setting.Tutorial.HasCompleted = tutorial.HasCompleted;

            return _setting;
        }

        public void Save(Setting @object)
        {
            SaveVolume(@object.Volume);
            SaveTutorial(@object.Tutorial);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(SettingDataKey.VolumeKey);
            PlayerPrefs.DeleteKey(SettingDataKey.TutorialKey);
        }

        private Volume LoadVolume() =>
            new (LoadData<VolumeData>(SettingDataKey.VolumeKey));

        private Tutorial LoadTutorial() =>
            new (LoadData<TutorialData>(SettingDataKey.TutorialKey));

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