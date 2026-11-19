using Presentation.UI.AudioSources;
using Presentation.Views;
using UnityEngine;

namespace Presentation.Containers.UI.AudioSources
{
    public class CongratulationUpgradeAudioSourceContainer : View
    {
        [field: SerializeField] public AudioSourceUI Charisma { get; private set; }
        [field: SerializeField] public AudioSourceUI Inventory { get; private set; }
        [field: SerializeField] public AudioSourceUI Movement { get; private set; }
    }
}