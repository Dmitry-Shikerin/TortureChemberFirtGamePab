using Sources.Presentation.UI.AudioSources;

namespace Sources.PresentationInterfaces.Views.Taverns.BackgroundMusics
{
    public interface IBackgroundMusicView
    {
        AudioSourceView BackgroundAudioSource { get; }
        AudioSourceView ButtonAudioSource { get; }
    }
}