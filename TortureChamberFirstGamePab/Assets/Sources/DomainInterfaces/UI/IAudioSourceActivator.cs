using System;

namespace Sources.DomainInterfaces.UI
{
    public interface IAudioSourceActivator
    {
        event Action AudioSourceActivated;
    }
}