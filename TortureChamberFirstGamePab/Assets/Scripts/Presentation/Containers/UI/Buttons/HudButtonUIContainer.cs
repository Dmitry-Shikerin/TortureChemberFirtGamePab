using Scripts.Presentation.UI.Buttons;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Buttons
{
    public class HudButtonUIContainer : MonoBehaviour
    {
        [field: SerializeField] public ButtonUI ContinueGameButton { get; private set; }
        [field: SerializeField] public ButtonUI NewGameButton { get; private set; }
        [field: SerializeField] public ButtonUI LeaderboardButton { get; private set; }
        [field: SerializeField] public ButtonUI BackToMainMenuButton { get; private set; }
        [field: SerializeField] public ButtonUI SettingButton { get; private set; }
        [field: SerializeField] public SettingFormButtonContainer SettingFormButtonContainer { get; private set; }
    }
}