using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.UI.Conteiners.Buttons
{
    public class TutorialFormButtonContainer : View
    {
        [field: SerializeField] public ButtonSoundUI CloseButton { get; private set; }
    }
}