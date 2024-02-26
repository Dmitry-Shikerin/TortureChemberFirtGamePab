using System;

namespace Sources.DomainInterfaces.UI.AudioSourcesActivators
{
    public interface IFourthAudioSourceActivator
    {
        bool IsActive { get; }
        
        event Action FirstAudioSourceActivated;
        event Action SecondAudioSourceActivated;
        event Action ThirdAudioSourceActivated;
        event Action FourthAudioSourceActivated;
    }
}