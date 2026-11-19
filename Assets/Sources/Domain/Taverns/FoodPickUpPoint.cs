using System;
using DomainInterfaces.UI.AudioSourcesActivators;

namespace Domain.Taverns
{
    public class FoodPickUpPoint : IDoubleAudioSourceActivator
    {
        public event Action FirstAudioSourceActivated;
        public event Action SecondAudioSourceActivated;

        public bool IsActive { get; private set; }

        public void StartAudioSource()
        {
            IsActive = true;
            FirstAudioSourceActivated?.Invoke();
        }

        public void StopAudioSource()
        {
            IsActive = false;
            SecondAudioSourceActivated?.Invoke();
        }
    }
}