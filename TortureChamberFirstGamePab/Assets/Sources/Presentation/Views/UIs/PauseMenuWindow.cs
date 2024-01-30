using Sources.Presentation.UI;
using UnityEngine;

namespace Sources.Presentation.Views.UIs
{
    public class PauseMenuWindow : View
    {
        [field: SerializeField] public ButtonUI SaveButton { get; private set; }
        [field: SerializeField] public ButtonUI QuitButton { get; private set; }
        [field: SerializeField] public ButtonUI CloseButton { get; private set; }
    }
}