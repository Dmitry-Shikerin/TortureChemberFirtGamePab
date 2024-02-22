using Sources.Presentation.UI.Buttons;

namespace Sources.PresentationInterfaces.Views.Forms.MainMenus
{
    public interface IAuthorizationFormView
    {
        ButtonView BackToMainMenuButton { get; }
        ButtonView AuthorizationButton { get; }
    }
}