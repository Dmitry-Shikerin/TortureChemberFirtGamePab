using Scripts.ControllersInterfaces;
using Scripts.PresentationInterfaces.Views.Forms.Common;

namespace Scripts.Presentation.Views.Forms.Common
{
    public class FormBase<T> : PresentableView<T>, IFormView
        where T : IPresenter
    {
    }
}