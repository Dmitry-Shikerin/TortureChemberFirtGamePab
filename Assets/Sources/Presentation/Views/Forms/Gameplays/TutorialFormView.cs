using Controllers.Forms.Gameplays;
using Presentation.UI.Buttons;
using Presentation.UI.ScrollViews;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.Gameplays;
using UnityEngine;

namespace Presentation.Views.Forms.Gameplays
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