using Scripts.Controllers.UI.AudioSources;
using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Scripts.Presentation.UI.AudioSources
{
    public class TripleAudioSourceUI : PresentableView<AudioSourceUIPresenterBase>, ITripleAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView FirstAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView SecondAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView ThirdAudioSourceView { get; private set; }
    }
}