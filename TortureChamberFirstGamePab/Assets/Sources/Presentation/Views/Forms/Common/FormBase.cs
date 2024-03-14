using Sources.ControllersInterfaces;
using Sources.PresentationInterfaces.Views.Forms;

namespace Sources.Presentation.Views.Forms.Common
{
    public class FormBase<T> : PresentableView<T>, IFormView
        where T : IPresenter
    {
    }
}