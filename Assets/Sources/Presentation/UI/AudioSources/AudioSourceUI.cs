using Controllers.UI.AudioSources;
using Presentation.Views;
using PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Presentation.UI.AudioSources
{
    public class AudioSourceUI : PresentableView<AudioSourceUIPresenterBase>, IAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView AudioSourceView { get; private set; }
    }
}