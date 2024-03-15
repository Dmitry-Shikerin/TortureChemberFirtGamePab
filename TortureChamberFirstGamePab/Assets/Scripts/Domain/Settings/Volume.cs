using System;
using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.SettingData;
using UnityEngine;

namespace Scripts.Domain.Settings
{
    public class Volume
    {
        private int _step;

        public Volume(VolumeData volumeData)
            : this(volumeData.Step)
        {
        }

        public Volume()
            : this(VolumeConstant.BaseStep)
        {
        }

        private Volume(int step)
        {
            Step = step;
        }

        public event Action VolumeChanged;

        public float VolumeValue => Step * VolumeConstant.VolumeValuePerStep;

        public int Step
        {
            get => _step;
            set
            {
                _step = Mathf.Clamp(value, 0, VolumeConstant.MaxStep);
                VolumeChanged?.Invoke();
            }
        }

        public void Increase()
        {
            Step++;
        }

        public void TurnDown()
        {
            Step--;
        }
    }
}