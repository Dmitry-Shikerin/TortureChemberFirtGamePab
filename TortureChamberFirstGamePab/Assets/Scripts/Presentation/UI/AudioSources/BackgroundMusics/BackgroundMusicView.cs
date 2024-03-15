using Scripts.Controllers.UI.AudioSources.BackgroundMusics;
using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.UI.AudioSources.BackgroundMusics;
using UnityEngine;

namespace Scripts.Presentation.UI.AudioSources.BackgroundMusics
{
    public class BackgroundMusicView : PresentableView<BackgroundMusicPresenter>, IBackgroundMusicView
    {
        [field: SerializeField] public AudioSourceView BackgroundMusic { get; private set; }
        [field: SerializeField] public AudioSourceView ButtonSound { get; private set; }
    }
}