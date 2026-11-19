using Controllers.Forms.Gameplays;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Presentation.Views.Forms.Gameplays
{
    public class UpgradeFormView : FormBase<UpgradeFormPresenter>, IUpgradeFormView
    {
        public void ShowHudForm() =>
            Presenter.ShowHudForm();
    }
}