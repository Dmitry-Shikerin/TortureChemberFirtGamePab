using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Buttons
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