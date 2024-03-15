using Scripts.Controllers.Forms.MainMenus;
using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms.MainMenus;
using UnityEngine;

namespace Scripts.Presentation.Views.Forms.MainMenus
{
    public class NewGameFormView : FormBase<NewGameFormPresenter>, INewGameFormView
    {
        [field: SerializeField] public ButtonView NewGameButton { get; private set; }
        [field: SerializeField] public ButtonView MainMenuButton { get; private set; }
    }
}