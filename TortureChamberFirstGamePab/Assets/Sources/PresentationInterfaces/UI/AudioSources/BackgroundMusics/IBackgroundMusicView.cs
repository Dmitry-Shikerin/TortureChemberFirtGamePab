using Sources.Presentation.UI.AudioSources;

namespace Sources.PresentationInterfaces.UI.AudioSources.BackgroundMusics
{
    public interface IBackgroundMusicView
    {
        AudioSourceView BackgroundMusic { get; }
        AudioSourceView ButtonSound { get; }
    }
}