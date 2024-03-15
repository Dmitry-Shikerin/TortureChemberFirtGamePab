using System;

namespace Scripts.DomainInterfaces.UI.AudioSourcesActivators
{
    public interface IAudioSourceActivator
    {
        event Action AudioSourceActivated;
    }
}