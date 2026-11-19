using Controllers.Forms.Gameplays;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Presentation.Views.Forms.Gameplays
{
    public class HudFormView : FormBase<HudFormPresenter>, IHudFormView
    {
        public void ShowPauseMenu() =>
            Presenter.ShowPauseMenu();
    }
}