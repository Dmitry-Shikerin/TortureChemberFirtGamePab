using System;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;
using UnityEngine;

namespace Sources.Domain.Taverns
{
    public class TavernMood
    {
        private const float StartTavernMoodValue = 0.5f;
        private const float MaximumUpgradeAmount = 0.25f;
        public const float AddedUpgradeAmount = 0.05f;
        
        private ObservableProperty<float> _tavernMoodValue = 
            new ObservableProperty<float>(StartTavernMoodValue);

        public float AddedAmountMood { get; private set; }
        private float _removedAmountMood ;

        public TavernMood()
        {
            AddedAmountMood = 0.1f;
            _removedAmountMood = 0.1f;
        }
        
        public IObservableProperty<float> TavernMoodValue => _tavernMoodValue;

        public void AddTavernMood()
        {
            _tavernMoodValue.Value += AddedAmountMood;
        }
        
        public void RemoveTavernMood()
        {
            _tavernMoodValue.Value -= _removedAmountMood;
        }

        //TODO вынести это в отдельный обьект и обобщить для всех апгрейдов
        public void AddAmountMood()
        {
            if (AddedAmountMood >= MaximumUpgradeAmount)
                throw new InvalidOperationException("Максимальное количество улучшения");
            
            AddedAmountMood += AddedUpgradeAmount;
            Debug.Log(AddedAmountMood);
        }
    }
}