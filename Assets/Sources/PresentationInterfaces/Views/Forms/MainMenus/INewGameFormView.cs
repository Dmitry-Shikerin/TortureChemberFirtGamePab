using Presentation.UI.Buttons;

namespace PresentationInterfaces.Views.Forms.MainMenus
{
    public interface INewGameFormView
    {
        ButtonView NewGameButton { get; }
        ButtonView MainMenuButton { get; }
    }
}