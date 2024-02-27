using Sources.Controllers.Forms.Gameplays;
using Sources.Presentation.UI.Buttons;
using Sources.Presentation.UI.ScrollViews;
using Sources.Presentation.Views.Forms.Common;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;
using UnityEngine;

namespace Sources.Presentation.Views.Forms.Gameplays
{
    public class TutorialFormView : FormBase<TutorialFormPresenter>, ITutorialFormView
    {
        [field: SerializeField] public float ScrollStep { get; private set; } = 0.1f;
        [field: SerializeField] public ButtonView UpScrollButton { get; private set; }
        [field: SerializeField] public ButtonView DownScrollButton { get; private set; }
        [field: SerializeField] public ScrollRectView ScrollRect { get; private set; }

        //TODO временное решение
        // [Button(ButtonSizes.Large, ButtonStyle.Box)]
        // public void ClearTutorial()
        // {
        //     Presenter.ClearCompleteTutorial();
        // }
        
        public void ShowPauseMenu() => 
            Presenter?.ShowPauseMenu();
    }
}