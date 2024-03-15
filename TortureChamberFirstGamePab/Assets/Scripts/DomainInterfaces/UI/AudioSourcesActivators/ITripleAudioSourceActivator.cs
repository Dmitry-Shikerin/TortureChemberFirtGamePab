using System;

namespace Scripts.DomainInterfaces.UI.AudioSourcesActivators
{
    public interface ITripleAudioSourceActivator
    {
        event Action FirstAudioSourceActivated;
        event Action SecondAudioSourceActivated;
        event Action ThirdAudioSourceActivated;
    }
}