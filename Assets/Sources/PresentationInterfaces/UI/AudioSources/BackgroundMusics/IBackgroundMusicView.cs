using Presentation.UI.AudioSources;

namespace PresentationInterfaces.UI.AudioSources.BackgroundMusics
{
    public interface IBackgroundMusicView
    {
        AudioSourceView BackgroundMusic { get; }
        AudioSourceView ButtonSound { get; }
    }
}