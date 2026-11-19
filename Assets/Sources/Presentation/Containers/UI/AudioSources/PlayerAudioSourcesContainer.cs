using Presentation.UI.AudioSources;
using Presentation.Views;
using UnityEngine;

namespace Presentation.Containers.UI.AudioSources
{
    public class PlayerAudioSourcesContainer : View
    {
        [field: SerializeField] public AudioSourceUI Wallet { get; private set; }
        [field: SerializeField] public TripleAudioSourceUI Inventory { get; private set; }
    }
}