using Sources.Controllers.Forms;
using UnityEngine;

namespace Sources.Presentation.Views.Forms.MainMenus.Containers
{
    public class MainMenuFormsContainer : View
    {
        [field: SerializeField] public MainMenuFormView MainMenuFormView { get; private set; }
        [field: SerializeField] public LeaderboardFormView LeaderboardFormView { get; private set; }
        [field: SerializeField] public SettingFormView SettingFormView { get; private set; }
    }
}