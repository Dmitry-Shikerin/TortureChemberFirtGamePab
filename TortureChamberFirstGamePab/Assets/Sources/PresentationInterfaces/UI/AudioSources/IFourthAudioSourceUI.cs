using Sources.Presentation.UI.AudioSources;

namespace Sources.PresentationInterfaces.UI.AudioSources
{
    public interface IFourthAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
        AudioSourceView ThirdAudioSourceView { get; }
        AudioSourceView FourthAudioSourceView { get; }
    }
}