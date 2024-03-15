using Scripts.Presentation.UI.AudioSources;

namespace Scripts.PresentationInterfaces.UI.AudioSources
{
    public interface ITripleAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
        AudioSourceView ThirdAudioSourceView { get; }
    }
}