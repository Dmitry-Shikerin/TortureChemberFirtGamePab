using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Buttons
{
    public class GameOverFormButtonContainer : View
    {
        [field: SerializeField] public ButtonUI BackToMainMenuButton { get; private set; }
    }
}