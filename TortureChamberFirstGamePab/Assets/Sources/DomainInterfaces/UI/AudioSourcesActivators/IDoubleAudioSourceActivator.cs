using System;

namespace Sources.DomainInterfaces.UI.AudioSourcesActivators
{
    public interface IDoubleAudioSourceActivator
    {
        bool IsActive { get; }
        event Action FirstAudioSourceActivated;
        event Action SecondAudioSourceActivated;
    }
}