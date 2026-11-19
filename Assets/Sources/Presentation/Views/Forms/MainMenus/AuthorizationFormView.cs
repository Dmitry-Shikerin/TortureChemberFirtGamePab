using Controllers.Forms.MainMenus;
using Presentation.UI.Buttons;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.MainMenus;
using UnityEngine;

namespace Presentation.Views.Forms.MainMenus
{
    public class AuthorizationFormView : FormBase<AuthorizationFormPresenter>, IAuthorizationFormView
    {
        [field: SerializeField] public ButtonView BackToMainMenuButton { get; private set; }
        [field: SerializeField] public ButtonView AuthorizationButton { get; private set; }
    }
}