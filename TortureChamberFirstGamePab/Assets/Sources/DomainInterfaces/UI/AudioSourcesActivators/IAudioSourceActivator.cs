using System;

namespace Sources.DomainInterfaces.UI.AudioSourcesActivators
{
    public interface IAudioSourceActivator
    {
        event Action AudioSourceActivated;
    }
}