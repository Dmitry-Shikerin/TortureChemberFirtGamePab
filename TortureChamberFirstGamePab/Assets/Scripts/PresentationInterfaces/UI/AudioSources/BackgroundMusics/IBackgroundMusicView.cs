using Scripts.Presentation.UI.AudioSources;

namespace Scripts.PresentationInterfaces.UI.AudioSources.BackgroundMusics
{
    public interface IBackgroundMusicView
    {
        AudioSourceView BackgroundMusic { get; }
        AudioSourceView ButtonSound { get; }
    }
}