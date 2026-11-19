using Presentation.Views;
using Presentation.Views.Forms;
using Presentation.Views.Forms.MainMenus;
using UnityEngine;

namespace Presentation.Containers.Forms
{
    public class MainMenuFormsContainer : View
    {
        [field: SerializeField] public MainMenuFormView MainMenuFormView { get; private set; }
        [field: SerializeField] public LeaderboardFormView LeaderboardFormView { get; private set; }
        [field: SerializeField] public SettingFormView SettingFormView { get; private set; }
        [field: SerializeField] public AuthorizationFormView AuthorizationFormView { get; private set; }
        [field: SerializeField] public NewGameFormView NewGameFormView { get; private set; }
    }
}