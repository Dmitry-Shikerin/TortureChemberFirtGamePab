using Presentation.UI.AudioSources;

namespace PresentationInterfaces.UI.AudioSources
{
    public interface ITripleAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
        AudioSourceView ThirdAudioSourceView { get; }
    }
}