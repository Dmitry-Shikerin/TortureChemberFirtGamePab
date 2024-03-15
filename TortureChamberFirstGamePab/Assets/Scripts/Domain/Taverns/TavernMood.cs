using System;
using Scripts.Domain.Constants;
using Scripts.Domain.DataAccess.TavernData;
using UnityEngine;

namespace Scripts.Domain.Taverns
{
    public class TavernMood
    {
        private float _tavernMoodValue;

        public TavernMood()
            : this(MoodValueConstant.StartValue)
        {
        }

        public TavernMood(TavernMoodData tavernMoodData)
            : this(tavernMoodData.MoodValue)
        {
        }

        private TavernMood(float tavernMoodValue)
        {
            TavernMoodValue = tavernMoodValue;
        }

        public event Action TavernMoodValueChanged;
        public event Action TavernMoodOver;

        public float AddedAmountUpgrade { get; set; }

        public float TavernMoodValue
        {
            get => _tavernMoodValue;
            private set
            {
                _tavernMoodValue = Mathf.Clamp(value, TavernMoodConstant.MaxValue, TavernMoodConstant.MaxValue);
                TavernMoodValueChanged?.Invoke();
            }
        }

        public void AddTavernMood() =>
            TavernMoodValue += AddedAmountUpgrade;

        public void RemoveTavernMood()
        {
            TavernMoodValue -= MoodValueConstant.RemovedAmount;

            if (TavernMoodValue <= 0)
                TavernMoodOver?.Invoke();
        }
    }
}