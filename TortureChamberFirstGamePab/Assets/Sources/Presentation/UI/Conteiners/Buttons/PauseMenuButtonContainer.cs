using Sources.Presentation.UI;
using Sources.Presentation.UI.Conteiners;
using UnityEngine;

namespace Sources.Presentation.Views.UIs
{
    public class PauseMenuButtonContainer : View
    {
        [field: SerializeField] public ButtonUI SaveButton { get; private set; }
        [field: SerializeField] public ButtonUI QuitButton { get; private set; }
        [field: SerializeField] public ButtonUI CloseButton { get; private set; }
        [field: SerializeField] public ButtonSoundUI TutorialButton { get; private set; }
    }
}