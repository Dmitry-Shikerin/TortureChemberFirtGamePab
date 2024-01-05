using UnityEngine;

namespace Sources.Presentation.UI.Conteiners
{
    public class HudTextUIContainer : MonoBehaviour
    {
        [field: SerializeField] public TextUI SystemErrorsText { get; private set; }
        [field: SerializeField] public TextUI PlayerWalletText { get; private set; }
    }
}