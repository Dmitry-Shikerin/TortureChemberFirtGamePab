using System;
using Sources.Domain.Constants;
using Sources.Domain.DataAccess.TavernData;
using UnityEngine;

namespace Sources.Domain.Taverns
{
    public class TavernMood
    {
        private float _tavernMoodValue;
        
        public TavernMood() : this(Constant.TavernMoodValues.StartValue)
        {
        }

        public TavernMood(TavernMoodData tavernMoodData) : this(tavernMoodData.MoodValue)
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
                _tavernMoodValue = Mathf.Clamp(value, 0, 1);
                TavernMoodValueChanged?.Invoke();
            }
        }

        public void AddTavernMood()
        {
            TavernMoodValue += AddedAmountUpgrade;
            
            Debug.Log($"{nameof(TavernMoodValue)} : {TavernMoodValue}");
            Debug.Log($"{nameof(AddedAmountUpgrade)} : {AddedAmountUpgrade}");
        }

        public void RemoveTavernMood()
        {
            TavernMoodValue -= Constant.TavernMoodValues.RemovedAmount;
            // TavernMoodValue -= 0.5f;
            if(TavernMoodValue <= 0)
                TavernMoodOver?.Invoke();
        }
    }
}