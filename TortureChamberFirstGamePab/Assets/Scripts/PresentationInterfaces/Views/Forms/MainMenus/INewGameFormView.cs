using Scripts.Presentation.UI.Buttons;

namespace Scripts.PresentationInterfaces.Views.Forms.MainMenus
{
    public interface INewGameFormView
    {
        ButtonView NewGameButton { get; }
        ButtonView MainMenuButton { get; }
    }
}