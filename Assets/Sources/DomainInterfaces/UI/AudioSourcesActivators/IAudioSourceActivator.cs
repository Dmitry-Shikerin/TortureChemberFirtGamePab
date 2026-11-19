using System;

namespace DomainInterfaces.UI.AudioSourcesActivators
{
    public interface IAudioSourceActivator
    {
        event Action AudioSourceActivated;
    }
}