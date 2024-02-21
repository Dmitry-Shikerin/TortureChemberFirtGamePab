using Sources.Presentation.UI.Buttons;
using UnityEngine.UI;

namespace Sources.PresentationInterfaces.Views.Forms.Gameplays
{
    public interface ITutorialFormView
    {
        float ScrollStep { get; }
        ButtonView UpScrollButton { get; }
        ButtonView DownScrollButton { get; }
        ScrollRect ScrollRect { get; }
        
        void ShowPauseMenu();
        void DownScroll(float step);
        void UpScroll(float step);
    }
}