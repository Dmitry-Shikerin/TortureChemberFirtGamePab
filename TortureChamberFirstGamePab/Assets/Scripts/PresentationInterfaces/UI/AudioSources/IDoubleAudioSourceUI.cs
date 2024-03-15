using Scripts.Presentation.UI.AudioSources;

namespace Scripts.PresentationInterfaces.UI.AudioSources
{
    public interface IDoubleAudioSourceUI
    {
        AudioSourceView FirstAudioSourceView { get; }
        AudioSourceView SecondAudioSourceView { get; }
    }
}