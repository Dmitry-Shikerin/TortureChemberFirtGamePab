using Scripts.PresentationInterfaces.Views;

namespace Scripts.Presentation.Views
{
    public class ContainerView : View
    {
        public void AppendChild(IView view) =>
            view.SetParent(transform);
    }
}