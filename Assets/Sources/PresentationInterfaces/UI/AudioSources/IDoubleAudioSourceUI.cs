using Presentation.UI.AudioSources;

namespace PresentationInterfaces.UI.AudioSources
{
    public interface IDoubleAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
    }
}