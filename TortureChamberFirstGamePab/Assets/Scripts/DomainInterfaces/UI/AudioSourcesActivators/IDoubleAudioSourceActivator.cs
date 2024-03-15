using System;

namespace Scripts.DomainInterfaces.UI.AudioSourcesActivators
{
    public interface IDoubleAudioSourceActivator
    {
        event Action FirstAudioSourceActivated;
        event Action SecondAudioSourceActivated;

        bool IsActive { get; }
    }
}