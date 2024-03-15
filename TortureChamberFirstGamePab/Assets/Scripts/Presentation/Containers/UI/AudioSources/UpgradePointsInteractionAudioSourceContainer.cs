using Scripts.Presentation.UI.AudioSources;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.AudioSources
{
    public class UpgradePointsInteractionAudioSourceContainer : View
    {
        [field: SerializeField] public AudioSourceUI Beer { get; private set; }
        [field: SerializeField] public AudioSourceUI Bread { get; private set; }
        [field: SerializeField] public AudioSourceUI Wine { get; private set; }
        [field: SerializeField] public AudioSourceUI Soup { get; private set; }
        [field: SerializeField] public AudioSourceUI Meat { get; private set; }
    }
}