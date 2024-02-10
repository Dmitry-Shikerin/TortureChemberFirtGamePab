using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;

namespace Sources.Domain.Taverns
{
    public class FoodPickUpPoint : IDoubleAudioSourceActivator
    {
        public event Action FirstAudioSourceActivated;
        public event Action SecondAudioSourceActivated;

        public void StartAudioSource() => 
            FirstAudioSourceActivated?.Invoke();

        public void StopAudioSource() => 
            SecondAudioSourceActivated?.Invoke();
    }
}