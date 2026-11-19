using PresentationInterfaces.Views;

namespace Presentation.Views
{
    public class ContainerView : View
    {
        public void AppendChild(IView view) =>
            view.SetParent(transform);
    }
}