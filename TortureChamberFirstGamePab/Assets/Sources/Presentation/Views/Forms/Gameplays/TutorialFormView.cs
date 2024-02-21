using Sources.Controllers.Forms.Gameplays;
using Sources.Presentation.UI.Buttons;
using Sources.Presentation.Views.Forms.Common;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.Views.Forms.Gameplays
{
    public class TutorialFormView : FormBase<TutorialFormPresenter>, ITutorialFormView
    {
        [field: SerializeField] public float ScrollStep { get; private set; } = 0.1f;
        [field: SerializeField] public ButtonView UpScrollButton { get; private set; }
        [field: SerializeField] public ButtonView DownScrollButton { get; private set; }
        [field: SerializeField] public ScrollRect ScrollRect { get; private set; }
        
        public void ShowPauseMenu() => 
            Presenter?.ShowPauseMenu();
        
        public void DownScroll(float step) => 
            ScrollRect.verticalNormalizedPosition -= step;

        public void UpScroll(float step) => 
            ScrollRect.verticalNormalizedPosition += step;
    }
}