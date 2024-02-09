using System;

namespace Sources.DomainInterfaces.UI.AudioSourcesActivators
{
    public interface IDoubleAudioSourceActivator
    {
        event Action FirstAudioSourceActivated;
        event Action SecondAudioSourceActivated;
    }
}