using Sources.Presentation.UI.Buttons;
using Sources.Presentation.UI.ScrollViews;
using UnityEngine.UI;

namespace Sources.PresentationInterfaces.Views.Forms.Gameplays
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