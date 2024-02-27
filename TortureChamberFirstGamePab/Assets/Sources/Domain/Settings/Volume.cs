using System;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.SettingData;
using UnityEngine;

namespace Sources.Domain.Settings
{
    public class Volume
    {
        private int _step;

        public Volume(VolumeData volumeData) : this(volumeData.Step)
        {
        }

        public Volume() : this(Constant.VolumeValue.BaseStep)
        {
        }

        private Volume(int step)
        {
            Step = step;
        }

        public event Action VolumeChanged;

        public float VolumeValue => Step * Constant.VolumeValue.VolumeValuePerStep;
        public int Step
        {
            get => _step; 
            set
            {
                _step = Mathf.Clamp(value, 0, Constant.VolumeValue.MaxStep);
                
                VolumeChanged?.Invoke();
            }
        }

        public void Increase() => 
            Step++;

        public void TurnDown() => 
            Step--;
    }
}