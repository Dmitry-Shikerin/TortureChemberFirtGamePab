using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Domain.Items.Garbages
{
    public class Garbage : IDoubleAudioSourceActivator
    {
        public IEatPointView EatPointView { get; private set; }
        public event Action FirstAudioSourceActivated;
        public event Action SecondAudioSourceActivated;

        public bool IsActive { get; private set; }

        public void SetEatPointView(IEatPointView eatPointView)
        {
            EatPointView = eatPointView;
        }

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