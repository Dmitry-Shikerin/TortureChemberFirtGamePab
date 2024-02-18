using Sources.Controllers.Forms.Gameplays;
using Sources.Presentation.Views.Forms.Common;
using Zenject;

namespace Sources.Presentation.Views.Forms.Gameplays
{
    public class PauseMenuFormView : FormBase<PauseMenuFormPresenter>, IPauseMenuFormView
    {
        public void ShowHudFormView() => 
            Presenter.ShowHudFormView();

        public void ShowTutorialFormView() => 
            Presenter.ShowTutorialFormView();

        public void ShowSettingsFormView() => 
            Presenter.ShowSettingsFormView();
    }
}