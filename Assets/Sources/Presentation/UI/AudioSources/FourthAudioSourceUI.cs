using Controllers.UI.AudioSources;
using Presentation.Views;
using PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Presentation.UI.AudioSources
{
    public class FourthAudioSourceUI : PresentableView<FourthAudioSourceUIPresenter>, IFourthAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView FirstAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView SecondAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView ThirdAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView FourthAudioSourceView { get; private set; }
    }
}