using Controllers.Forms.Gameplays;
using Presentation.Views.Forms.Common;
using PresentationInterfaces.Views.Forms.Gameplays;

namespace Presentation.Views.Forms.Gameplays
{
    public class LoadFormView : FormBase<LoadFormPresenter>, ILoadFormView
    {
        public void ShowHudForm() =>
            Presenter.ShowHudForm();
    }
}