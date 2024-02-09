using Sources.Controllers.UI.AudioSources;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Sources.Presentation.UI.AudioSources
{
    public class DoubleAudioSourceUI : PresentableView<DoubleAudioSourceUIPresenter>, IDoubleAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView FirstAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView SecondAudioSourceView { get; private set; }
    }
}