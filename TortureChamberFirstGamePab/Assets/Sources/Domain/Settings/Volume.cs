using System;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.SettingData;
using UnityEngine;

namespace Sources.Domain.Settings
{
    public class Volume
    {
        public event Action VolumeChanged;
        
        //TODO сменить магические числа
        public Volume(VolumeData volumeData) : this(volumeData.VolumeValue, volumeData.Step)
        {
        }
        
        public Volume() : this(Constant.VolumeValue.BaseValue, Constant.VolumeValue.BaseStep)
        {
        }
        
        private Volume(float volumeValue, int step)
        {
            VolumeValue = volumeValue;
            Step = step;
        }
        
        public float VolumeValue { get; private set; }
        
        public int Step { get; private set; }

        //TODO Заменить магические числа
        public void Increase()
        {
            if (VolumeValue >= Constant.VolumeValue.MaxValue)
            {
                VolumeValue = Constant.VolumeValue.MaxValue;
                return;
            }
            
            VolumeValue += Constant.VolumeValue.VolumeValuePerStep;
            Step++;
            
            VolumeChanged?.Invoke();
            
            if (VolumeValue >= Constant.VolumeValue.MaxValue)
            {
                VolumeValue = Constant.VolumeValue.MaxValue;
            }
            Debug.Log($"Volume Increase {VolumeValue}");
        }

        public void TurnDown()
        {
            if (VolumeValue <= Constant.VolumeValue.MinValue)
            {
                VolumeValue = Constant.VolumeValue.MinValue;
                return;
            }

            if (Step <= Constant.VolumeValue.MinStep)
            {
                return;
            }
            
            VolumeValue -= Constant.VolumeValue.VolumeValuePerStep;
            Step--;
            
            VolumeChanged?.Invoke();
            
            if (VolumeValue <= Constant.VolumeValue.MinValue)
            {
                VolumeValue = Constant.VolumeValue.MinValue;
            }
            Debug.Log($"Volume TornDown {VolumeValue}");
        }
    }
}