using Sources.Controllers.UI.AudioSources;
using Sources.Presentation.UI.AudioSources;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.UI.Conteiners.AudioSources
{
    public class PlayerAudioSourcesContainer : View
    {
        [field: SerializeField] public AudioSourceUI Wallet { get; private set; }
        [field: SerializeField] public TripleAudioSourceUI Inventory { get; private set; }
    }
}