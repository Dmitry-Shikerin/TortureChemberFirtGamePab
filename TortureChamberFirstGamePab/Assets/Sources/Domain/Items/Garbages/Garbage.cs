using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Domain.Items.Garbages
{
    public class Garbage : IDoubleAudioSourceActivator
    {
        public event Action FirstAudioSourceActivated;
        public event Action SecondAudioSourceActivated;
        
        public IEatPointView EatPointView { get; private set; }


        public void SetEatPointView(IEatPointView eatPointView) => 
            EatPointView = eatPointView;

        public void StartAudioSource() => 
            FirstAudioSourceActivated?.Invoke();

        public void StopAudioSource() => 
            SecondAudioSourceActivated?.Invoke();
    }
}