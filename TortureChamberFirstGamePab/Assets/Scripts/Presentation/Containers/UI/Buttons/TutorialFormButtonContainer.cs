using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Buttons
{
    public class TutorialFormButtonContainer : View
    {
        [field: SerializeField] public ButtonSoundUI CloseButton { get; private set; }
    }
}