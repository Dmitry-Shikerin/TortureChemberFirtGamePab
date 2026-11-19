using Presentation.UI.Buttons;
using Presentation.Views;
using UnityEngine;

namespace Presentation.Containers.UI.Buttons
{
    public class TutorialFormButtonContainer : View
    {
        [field: SerializeField] public ButtonSoundUI CloseButton { get; private set; }
    }
}