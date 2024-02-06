﻿using System;
using Sources.Domain.Constants;
using Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData;
using UnityEngine;

namespace Sources.Domain.Taverns
{
    public class TavernMood
    {
        private float _tavernMoodValue;

        public event Action TavernMoodValueChanged; 

        public TavernMood() : this(Constant.TavernMoodValues.StartValue)
        {
        }

        public TavernMood(TavernMoodData tavernMoodData) : this(tavernMoodData.MoodValue)
        {
        }

        private TavernMood(float tavernMoodValue)
        {
            _tavernMoodValue = tavernMoodValue;
        }

        public float AddedAmountUpgrade { get; set; }
        
        public float TavernMoodValue
        {
            get => _tavernMoodValue;
            private set
            {
                _tavernMoodValue = value;
                TavernMoodValueChanged?.Invoke();
            }
        }

        public void AddTavernMood() => 
            TavernMoodValue += AddedAmountUpgrade;

        public void RemoveTavernMood() => 
            TavernMoodValue -= Constant.TavernMoodValues.RemovedAmount;
    }
}