using Scripts.Controllers.Forms.Gameplays;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms.Gameplays;

namespace Scripts.Presentation.Views.Forms.Gameplays
{
    public class UpgradeFormView : FormBase<UpgradeFormPresenter>, IUpgradeFormView
    {
        public void ShowHudForm() =>
            Presenter.ShowHudForm();
    }
}