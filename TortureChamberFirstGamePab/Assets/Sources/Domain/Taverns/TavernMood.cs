using System;
using Sources.Domain.Constants;

namespace Sources.Domain.Taverns
{
    public class TavernMood
    {
        private float _tavernMoodValue;

        public event Action TavernMoodValueChanged; 

        public TavernMood()
        {
            _tavernMoodValue = Constant.StartTavernMoodValue;
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
            _tavernMoodValue += AddedAmountUpgrade;

        public void RemoveTavernMood() => 
            _tavernMoodValue -= Constant.RemovedAmountMood;
    }
}