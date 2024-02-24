using System;
using Sources.Domain.Constants;
using Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData;
using UnityEngine;

namespace Sources.Domain.Taverns
{
    public class TavernMood
    {
        public event Action TavernMoodValueChanged;
        public event Action TavernMoodOver;

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

        public float AddedAmountUpgrade { get; set; }
        public float TavernMoodValue { get; private set; }

        public void AddTavernMood()
        {
            TavernMoodValue += AddedAmountUpgrade;
            
            TavernMoodValueChanged?.Invoke();
            
        }

        public void RemoveTavernMood()
        {
            // TavernMoodValue -= Constant.TavernMoodValues.RemovedAmount;
            TavernMoodValue -= 0.5f;
            
            TavernMoodValueChanged?.Invoke();
            
            if(TavernMoodValue <= 0)
                TavernMoodOver?.Invoke();
        }
    }
}