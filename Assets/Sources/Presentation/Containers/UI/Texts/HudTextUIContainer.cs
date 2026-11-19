using Presentation.UI.Texts;
using UnityEngine;

namespace Presentation.Containers.UI.Texts
{
    public class HudTextUIContainer : MonoBehaviour
    {
        [field: SerializeField] public TextUI SystemErrorsText { get; private set; }
        [field: SerializeField] public TextUI PlayerWalletText { get; private set; }
    }
}