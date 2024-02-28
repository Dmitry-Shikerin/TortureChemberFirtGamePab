using Sources.Controllers.UI.AudioSources.BackgroundMusics;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.UI.AudioSources.BackgroundMusics;
using UnityEngine;

namespace Sources.Presentation.UI.AudioSources.BackgroundMusics
{
    public class BackgroundMusicView : PresentableView<BackgroundMusicPresenter>, IBackgroundMusicView
    {
        [field: SerializeField] public AudioSourceView BackgroundMusic { get; private set; }
        [field: SerializeField] public AudioSourceView ButtonSound { get; private set; }
    }
}