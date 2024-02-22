using Sources.Controllers.Forms.MainMenus;
using Sources.Presentation.UI.Buttons;
using Sources.Presentation.Views.Forms.Common;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;
using UnityEngine;

namespace Sources.Presentation.Views.Forms.MainMenus
{
    public class AuthorizationFormView : FormBase<AuthorizationFormPresenter>, IAuthorizationFormView
    {
        [field: SerializeField] public ButtonView BackToMainMenuButton { get; private set; }
        [field: SerializeField] public ButtonView AuthorizationButton { get; private set; }
    }
}