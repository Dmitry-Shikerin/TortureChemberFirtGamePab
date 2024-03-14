using System;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.DataAccess.SettingData;
using Sources.Domain.Settings;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using UnityEngine;

namespace Sources.Infrastructure.Services.LoadServices.Components
{
    public class SettingDataService : DataServiceBase, IDataService<Setting>
    {
        private readonly Setting _setting;

        public SettingDataService(Setting setting)
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
        }

        private bool CanLoadTutorial => PlayerPrefs.HasKey(Constant.SettingDataKey.TutorialKey);

        public bool CanLoad => PlayerPrefs.HasKey(Constant.SettingDataKey.VolumeKey);

        public Setting Load()
        {
            if (CanLoad == false)
                return _setting;

            var volume = LoadVolume();
            _setting.Volume.Step = volume.Step;

            if (CanLoadTutorial == false)
                return _setting;

            var tutorial = LoadTutorial();
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
            PlayerPrefs.DeleteKey(Constant.SettingDataKey.VolumeKey);
            PlayerPrefs.DeleteKey(Constant.SettingDataKey.TutorialKey);
        }

        private Volume LoadVolume()
        {
            return new Volume(LoadData<VolumeData>(Constant.SettingDataKey.VolumeKey));
        }

        private Tutorial LoadTutorial()
        {
            return new Tutorial(LoadData<TutorialData>(Constant.SettingDataKey.TutorialKey));
        }

        private void SaveVolume(Volume volume)
        {
            var volumeData = new VolumeData
            {
                Step = volume.Step
            };

            SaveData(volumeData, Constant.SettingDataKey.VolumeKey);
        }

        private void SaveTutorial(Tutorial tutorial)
        {
            var tutorialData = new TutorialData
            {
                HasCompleted = tutorial.HasCompleted
            };

            SaveData(tutorialData, Constant.SettingDataKey.TutorialKey);
        }
    }
}