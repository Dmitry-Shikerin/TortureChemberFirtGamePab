using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;

namespace Sources.Domain.Taverns
{
    public class FoodPickUpPoint : IDoubleAudioSourceActivator
    {
        public event Action FirstAudioSourceActivated;
        public event Action SecondAudioSourceActivated;

        public bool IsActive { get; set; }

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