using System;

namespace Sources.DomainInterfaces.UI.AudioSourcesActivators
{
    public interface ITripleAudioSourceActivator
    {
        event Action FirstAudioSourceActivated;
        event Action SecondAudioSourceActivated;
        event Action ThirdAudioSourceActivated;
    }
}