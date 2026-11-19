using Controllers.UI.AudioSources;
using Presentation.Views;
using PresentationInterfaces.UI.AudioSources;
using UnityEngine;

namespace Presentation.UI.AudioSources
{
    public class TripleAudioSourceUI : PresentableView<AudioSourceUIPresenterBase>, ITripleAudioSourceUI
    {
        [field: SerializeField] public AudioSourceView FirstAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView SecondAudioSourceView { get; private set; }
        [field: SerializeField] public AudioSourceView ThirdAudioSourceView { get; private set; }
    }
}