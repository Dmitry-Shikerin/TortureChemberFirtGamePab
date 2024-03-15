using Scripts.Presentation.UI.Texts;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Texts
{
    public class HudTextUIContainer : MonoBehaviour
    {
        [field: SerializeField] public TextUI SystemErrorsText { get; private set; }
        [field: SerializeField] public TextUI PlayerWalletText { get; private set; }
    }
}