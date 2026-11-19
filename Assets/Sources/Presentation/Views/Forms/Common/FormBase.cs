using ControllersInterfaces;
using PresentationInterfaces.Views.Forms.Common;

namespace Presentation.Views.Forms.Common
{
    public class FormBase<T> : PresentableView<T>, IFormView
        where T : IPresenter
    {
    }
}