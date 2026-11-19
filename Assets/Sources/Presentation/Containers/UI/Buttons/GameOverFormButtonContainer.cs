using Presentation.UI.Buttons;
using Presentation.Views;
using UnityEngine;

namespace Presentation.Containers.UI.Buttons
{
    public class GameOverFormButtonContainer : View
    {
        [field: SerializeField] public ButtonUI BackToMainMenuButton { get; private set; }
    }
}