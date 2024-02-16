using Sources.Presentation.UI;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.Containers.UI.Buttons
{
    public class GameOverFormButtonContainer : View
    {
        [field: SerializeField] public ButtonUI BackToMainMenuButton { get; private set; }
    }
}