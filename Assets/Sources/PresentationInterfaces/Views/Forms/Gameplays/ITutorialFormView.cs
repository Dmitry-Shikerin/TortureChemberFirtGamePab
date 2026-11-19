using Presentation.UI.Buttons;
using Presentation.UI.ScrollViews;

namespace PresentationInterfaces.Views.Forms.Gameplays
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