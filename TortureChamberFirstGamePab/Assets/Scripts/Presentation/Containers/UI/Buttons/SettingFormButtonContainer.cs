using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.Views;
using UnityEngine;

namespace Scripts.Presentation.Containers.UI.Buttons
{
    public class SettingFormButtonContainer : View
    {
        [field: SerializeField] public ButtonUI BackToMainMenu { get; private set; }
        [field: SerializeField] public ButtonUI IncreaseVolume { get; private set; }
        [field: SerializeField] public ButtonUI TornDownVolume { get; private set; }
    }
}