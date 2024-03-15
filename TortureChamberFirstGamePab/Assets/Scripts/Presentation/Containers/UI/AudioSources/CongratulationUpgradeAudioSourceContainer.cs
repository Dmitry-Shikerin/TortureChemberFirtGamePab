using Scripts.Presentation.UI.AudioSources;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.AudioSources
{
    public class CongratulationUpgradeAudioSourceContainer : View
    {
        [field: SerializeField] public AudioSourceUI Charisma { get; private set; }
        [field: SerializeField] public AudioSourceUI Inventory { get; private set; }
        [field: SerializeField] public AudioSourceUI Movement { get; private set; }
    }
}