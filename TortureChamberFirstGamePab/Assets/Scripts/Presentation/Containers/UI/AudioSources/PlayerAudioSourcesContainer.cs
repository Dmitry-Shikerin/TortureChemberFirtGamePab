using Scripts.Presentation.UI.AudioSources;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.AudioSources
{
    public class PlayerAudioSourcesContainer : View
    {
        [field: SerializeField] public AudioSourceUI Wallet { get; private set; }
        [field: SerializeField] public TripleAudioSourceUI Inventory { get; private set; }
    }
}