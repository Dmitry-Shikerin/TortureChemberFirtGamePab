using Sources.Presentation.UI.AudioSources;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.UI.Conteiners.AudioSources
{
    public class CongratulationUpgradeAudioSourceContainer : View
    {
        [field: SerializeField] public AudioSourceUI Charisma { get; private set; }
        [field: SerializeField] public AudioSourceUI Inventory { get; private set; }
        [field: SerializeField] public AudioSourceUI Movement { get; private set; }
        // [field: SerializeField] public AudioSourceUI Advertisement { get; private set; }
    }
}