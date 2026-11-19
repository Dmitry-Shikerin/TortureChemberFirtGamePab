using Presentation.UI.Buttons;

namespace PresentationInterfaces.Views.Forms.MainMenus
{
    public interface IAuthorizationFormView
    {
        ButtonView BackToMainMenuButton { get; }
        ButtonView AuthorizationButton { get; }
    }
}