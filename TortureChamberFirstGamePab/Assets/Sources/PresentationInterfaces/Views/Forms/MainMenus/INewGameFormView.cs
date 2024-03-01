using Sources.Presentation.UI.Buttons;

namespace Sources.PresentationInterfaces.Views.Forms.MainMenus
{
    public interface INewGameFormView
    {
        ButtonView NewGameButton { get; }
        ButtonView MainMenuButton { get; }
    }
}