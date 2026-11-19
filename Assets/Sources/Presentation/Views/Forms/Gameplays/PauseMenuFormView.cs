using Controllers.Forms.Gameplays;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Presentation.Views.Forms.Gameplays
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