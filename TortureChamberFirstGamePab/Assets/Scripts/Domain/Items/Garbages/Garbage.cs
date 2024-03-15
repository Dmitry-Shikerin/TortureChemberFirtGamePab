using System;
using Scripts.DomainInterfaces.UI.AudioSourcesActivators;
using Scripts.PresentationInterfaces.Views.Points;

namespace Scripts.Domain.Items.Garbages
{
    public class Garbage : IDoubleAudioSourceActivator
    {
        public event Action FirstAudioSourceActivated;
        public event Action SecondAudioSourceActivated;

        public IEatPointView EatPointView { get; private set; }
        public bool IsActive { get; private set; }

        public void SetEatPointView(IEatPointView eatPointView) =>
            EatPointView = eatPointView;

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