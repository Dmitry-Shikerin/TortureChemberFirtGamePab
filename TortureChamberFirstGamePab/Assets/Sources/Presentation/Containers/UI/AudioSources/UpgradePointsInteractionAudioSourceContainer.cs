using Sources.Presentation.UI.AudioSources;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.UI.Conteiners.AudioSources
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