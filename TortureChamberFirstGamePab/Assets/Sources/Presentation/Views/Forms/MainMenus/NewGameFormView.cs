using Sources.Controllers.Forms.MainMenus;
using Sources.Presentation.UI.Buttons;
using Sources.Presentation.Views.Forms.Common;
using Sources.PresentationInterfaces.Views.Forms.MainMenus;
using UnityEngine;

namespace Sources.Presentation.Views.Forms.MainMenus
{
    public class NewGameFormView : FormBase<NewGameFormPresenter>, INewGameFormView
    {
        [field: SerializeField] public ButtonView NewGameButton { get; private set; }
        [field: SerializeField] public ButtonView MainMenuButton { get; private set; }
    }
}