using Scripts.Controllers.UI.AudioSources;
using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Scripts.Presentation.UI.AudioSources
{
    public class AudioSourceUI : PresentableView<AudioSourceUIPresenterBase>, IAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView AudioSourceView { get; private set; }
    }
}