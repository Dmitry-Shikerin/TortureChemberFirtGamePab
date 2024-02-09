using Sources.Presentation.UI.AudioSources;

namespace Sources.PresentationInterfaces.UI.AudioSources
{
    public interface IDoubleAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
    }
}