using Scripts.Controllers.Forms.Gameplays;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms.Gameplays;

namespace Scripts.Presentation.Views.Forms.Gameplays
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