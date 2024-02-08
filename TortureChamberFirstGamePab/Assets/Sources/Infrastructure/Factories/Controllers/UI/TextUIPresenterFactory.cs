using System;
using Sources.Controllers.UI;
using Sources.PresentationInterfaces.UI;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;

namespace Sources.Infrastructure.Factories.Controllers.UI
{
    public class TextUIPresenterFactory
    {
        public TextUIPresenter Create(ITextUI textUI, IObservableProperty observableProperty)
        {
            if (textUI == null) 
                throw new ArgumentNullException(nameof(textUI));
            
            if (observableProperty == null) 
                throw new ArgumentNullException(nameof(observableProperty));
            
            return new TextUIPresenter(textUI, observableProperty);
        }
    }
}