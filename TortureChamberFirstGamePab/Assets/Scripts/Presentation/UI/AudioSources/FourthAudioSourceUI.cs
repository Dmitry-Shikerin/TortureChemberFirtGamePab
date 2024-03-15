using Scripts.Controllers.UI.AudioSources;
using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Scripts.Presentation.UI.AudioSources
{
    public class FourthAudioSourceUI : PresentableView<FourthAudioSourceUIPresenter>, IFourthAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView FirstAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView SecondAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView ThirdAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView FourthAudioSourceView { get; private set; }
    }
}