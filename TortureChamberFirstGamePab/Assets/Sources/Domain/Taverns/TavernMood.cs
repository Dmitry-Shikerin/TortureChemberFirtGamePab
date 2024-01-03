using System;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;
using UnityEngine;

namespace Sources.Domain.Taverns
{
    public class TavernMood
    {
        //TODO убрать магические числа
        private ObservableProperty<float> _tavernMoodValue = new ObservableProperty<float>(0.5f);

        private float _addedAmountMood ;
        private float _removedAmountMood ;

        public TavernMood()
        {
            _addedAmountMood = 0.1f;
            _removedAmountMood = 0.1f;
        }
        
        public IObservableProperty<float> TavernMoodValue => _tavernMoodValue;

        public void AddTavernMood()
        {
            _tavernMoodValue.Value += _addedAmountMood;
        }
        
        public void RemoveTavernMood()
        {
            _tavernMoodValue.Value -= _removedAmountMood;
        }

        public void AddAmountMood()
        {
            if (_addedAmountMood >= 0.25f)
                throw new InvalidOperationException("Максимальное количество улучшения");
            
            _addedAmountMood += 0.05f;
            Debug.Log(_addedAmountMood);
        }
        
        public void GetTavernMoodValue()
        {
            
        }
    }
}