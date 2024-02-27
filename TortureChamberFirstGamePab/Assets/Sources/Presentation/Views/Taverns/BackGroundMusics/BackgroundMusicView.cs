using Sources.Controllers.Taverns;
using Sources.Presentation.UI.AudioSources;
using Sources.PresentationInterfaces.Views.Taverns.BackgroundMusics;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns.BackGroundMusics
{
    public class BackgroundMusicView : PresentableView<BackgroundMusicPresenter>, IBackgroundMusicView
    {
        [field: SerializeField] public AudioSourceView BackgroundAudioSource { get; private set; }
        [field: SerializeField] public AudioSourceView ButtonAudioSource { get; private set; }
    }
}