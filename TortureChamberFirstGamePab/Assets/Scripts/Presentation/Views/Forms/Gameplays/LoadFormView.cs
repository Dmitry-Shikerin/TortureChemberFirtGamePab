using Scripts.Controllers.Forms.Gameplays;
using Scripts.Presentation.Views.Forms.Common;
using Scripts.PresentationInterfaces.Views.Forms.Gameplays;

namespace Scripts.Presentation.Views.Forms.Gameplays
{
    public class LoadFormView : FormBase<LoadFormPresenter>, ILoadFormView
    {
        public void ShowHudForm() =>
            Presenter.ShowHudForm();
    }
}