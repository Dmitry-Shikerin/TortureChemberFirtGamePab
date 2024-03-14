using Sources.Controllers.UI.AudioSources;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Sources.Presentation.UI.AudioSources
{
    public class AudioSourceUI : PresentableView<AudioSourceUIPresenterBase>, IAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView AudioSourceView { get; private set; }
    }
}