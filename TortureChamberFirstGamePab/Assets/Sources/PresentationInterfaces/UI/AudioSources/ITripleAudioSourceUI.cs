using Sources.Presentation.UI.AudioSources;

namespace Sources.PresentationInterfaces.UI.AudioSources
{
    public interface ITripleAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
        AudioSourceView ThirdAudioSourceView { get; }
    }
}