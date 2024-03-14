using Sources.Controllers.Forms.Gameplays;
using Sources.Presentation.Views.Forms.Common;

namespace Sources.Presentation.Views.Forms.Gameplays
{
    public class LoadFormView : FormBase<LoadFormPresenter>, ILoadFormView
    {
        public void ShowHudForm()
        {
            Presenter.ShowHudForm();
        }
    }
}