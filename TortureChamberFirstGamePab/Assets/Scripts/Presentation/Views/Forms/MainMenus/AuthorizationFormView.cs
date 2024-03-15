using Scripts.Controllers.Forms.MainMenus;
using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms.MainMenus;
using UnityEngine;

namespace Scripts.Presentation.Views.Forms.MainMenus
{
    public class AuthorizationFormView : FormBase<AuthorizationFormPresenter>, IAuthorizationFormView
    {
        [field: SerializeField] public ButtonView BackToMainMenuButton { get; private set; }
        [field: SerializeField] public ButtonView AuthorizationButton { get; private set; }
    }
}