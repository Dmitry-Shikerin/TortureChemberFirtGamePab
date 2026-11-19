using Controllers.Forms.MainMenus;
using Presentation.UI.Buttons;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.MainMenus;
using UnityEngine;

namespace Presentation.Views.Forms.MainMenus
{
    public class NewGameFormView : FormBase<NewGameFormPresenter>, INewGameFormView
    {
        [field: SerializeField] public ButtonView NewGameButton { get; private set; }
        [field: SerializeField] public ButtonView MainMenuButton { get; private set; }
    }
}