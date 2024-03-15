using Scripts.Presentation.UI.AudioSources;

namespace Scripts.PresentationInterfaces.UI.AudioSources
{
    public interface IFourthAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
        AudioSourceView ThirdAudioSourceView { get; }
        AudioSourceView FourthAudioSourceView { get; }
    }
}