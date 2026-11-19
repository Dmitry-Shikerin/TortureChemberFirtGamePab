using Presentation.UI.AudioSources;

namespace PresentationInterfaces.UI.AudioSources
{
    public interface IFourthAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
        AudioSourceView ThirdAudioSourceView { get; }
        AudioSourceView FourthAudioSourceView { get; }
    }
}