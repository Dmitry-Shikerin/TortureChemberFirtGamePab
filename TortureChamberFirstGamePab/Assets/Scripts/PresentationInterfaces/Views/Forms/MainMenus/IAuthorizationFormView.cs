using Scripts.Presentation.UI.Buttons;

namespace Scripts.PresentationInterfaces.Views.Forms.MainMenus
{
    public interface IAuthorizationFormView
    {
        ButtonView BackToMainMenuButton { get; }
        ButtonView AuthorizationButton { get; }
    }
}