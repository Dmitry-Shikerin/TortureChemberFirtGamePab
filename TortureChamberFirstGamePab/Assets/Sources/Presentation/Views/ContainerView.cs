using Sources.PresentationInterfaces.Views;

namespace Sources.Presentation.Views
{
    public class ContainerView : View
    {
        public void AppendChild(IView view) => 
            view.SetParent(transform);
    }
}