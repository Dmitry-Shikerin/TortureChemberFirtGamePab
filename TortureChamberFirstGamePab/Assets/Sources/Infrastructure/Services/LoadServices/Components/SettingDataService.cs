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
    public class SettingDataService : DataServiceBase, IDataService<Setting>
    {
        private readonly Setting _setting;

        public SettingDataService(Setting setting)
        {
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
        }

        public bool CanLoad => PlayerPrefs.HasKey(Constant.SettingDataKey.VolumeKey);

        private bool CanLoadTutorial => PlayerPrefs.HasKey(Constant.SettingDataKey.TutorialKey);

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
            PlayerPrefs.DeleteKey(Constant.SettingDataKey.VolumeKey);
            PlayerPrefs.DeleteKey(Constant.SettingDataKey.TutorialKey);
            Debug.Log("SettingDataService Clear");
        }

        private Volume LoadVolume() => 
            new(LoadData<VolumeData>(Constant.SettingDataKey.VolumeKey));

        private Tutorial LoadTutorial() => 
            new(LoadData<TutorialData>(Constant.SettingDataKey.TutorialKey));

        private void SaveVolume(Volume volume)
        {
            VolumeData volumeData = new VolumeData()
            {
                Step = volume.Step,
            };

            SaveData(volumeData, Constant.SettingDataKey.VolumeKey);
        }

        private void SaveTutorial(Tutorial tutorial)
        {
            TutorialData tutorialData = new TutorialData()
            {
                 HasCompleted = tutorial.HasCompleted,
            };

            SaveData(tutorialData, Constant.SettingDataKey.VolumeKey);
        }
    }
}