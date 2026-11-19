using Presentation.UI.Buttons;
using Presentation.Views;
using UnityEngine;

namespace Presentation.Containers.UI.Buttons
{
    public class PauseMenuButtonContainer : View
    {
        [field: SerializeField] public ButtonUI SaveButton { get; private set; }
        [field: SerializeField] public ButtonUI MainMenuButton { get; private set; }
        [field: SerializeField] public ButtonUI CloseButton { get; private set; }
        [field: SerializeField] public ButtonSoundUI TutorialButton { get; private set; }
        [field: SerializeField] public ButtonSoundUI AdvertisementButton { get; private set; }
        [field: SerializeField] public ButtonSoundUI SettingsButton { get; private set; }
    }
}