using Scripts.Controllers.Forms.Gameplays;
using Scripts.Presentation.UI.Buttons;
using Scripts.Presentation.UI.ScrollViews;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms.Gameplays;
using UnityEngine;

namespace Scripts.Presentation.Views.Forms.Gameplays
{
    public class TutorialFormView : FormBase<TutorialFormPresenter>, ITutorialFormView
    {
        [field: SerializeField] public float ScrollStep { get; private set; } = 0.1f;
        [field: SerializeField] public ButtonView UpScrollButton { get; private set; }
        [field: SerializeField] public ButtonView DownScrollButton { get; private set; }
        [field: SerializeField] public ScrollRectView ScrollRect { get; private set; }

        public void ShowPauseMenu() =>
            Presenter?.ShowPauseMenu();
    }
}