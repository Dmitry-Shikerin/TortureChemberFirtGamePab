using Sources.Presentation.UI;
using Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Presentation.Containers.UI.Buttons
{
    public class SettingFormButtonContainer : View
    {
        [field: SerializeField] public ButtonUI BackToMainMenu { get; private set; }
        [field: SerializeField] public ButtonUI IncreaseVolume { get; private set; }
        [field: SerializeField] public ButtonUI TornDownVolume { get; private set; }
    }
}