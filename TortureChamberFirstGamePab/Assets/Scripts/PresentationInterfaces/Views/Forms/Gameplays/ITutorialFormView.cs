using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.UI.ScrollViews;

namespace Scripts.PresentationInterfaces.Views.Forms.Gameplays
{
    public interface ITutorialFormView
    {
        float ScrollStep { get; }
        ButtonView UpScrollButton { get; }
        ButtonView DownScrollButton { get; }
        ScrollRectView ScrollRect { get; }

        void ShowPauseMenu();
    }
}