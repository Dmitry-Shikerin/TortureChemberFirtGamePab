using Sources.Controllers.Forms.Gameplays;
using Sources.Presentation.Views.Forms.Common;

namespace Sources.Presentation.Views.Forms.Gameplays
{
    public class UpgradeFormView : FormBase<UpgradeFormPresenter>, IUpgradeFormView
    {
        public void ShowHudForm()
        {
            Presenter.ShowHudForm();
        }
    }
}