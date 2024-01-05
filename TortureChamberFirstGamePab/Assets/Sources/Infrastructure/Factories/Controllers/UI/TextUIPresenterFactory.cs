using MyProject.Sources.Controllers.UI;
using Sources.PresentationInterfaces.UI;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;

namespace Sources.Infrastructure.Factories.Controllers.UI
{
    public class TextUIPresenterFactory
    {
        public TextUIPresenter Create(ITextUI textUI, IObservableProperty observableProperty)
        {
            return new TextUIPresenter(textUI, observableProperty);
        }
    }
}