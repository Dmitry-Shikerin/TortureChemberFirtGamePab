using Sources.Controllers.Forms.Gameplays;
using Sources.Presentation.Views.Forms.Common;
using Sources.PresentationInterfaces.Views.Forms.Gameplays;

namespace Sources.Presentation.Views.Forms.Gameplays
{
    public class HudFormView : FormBase<HudFormPresenter>, IHudFormView
    {
        public void ShowPauseMenu() => 
            Presenter.ShowPauseMenu();
    }
}