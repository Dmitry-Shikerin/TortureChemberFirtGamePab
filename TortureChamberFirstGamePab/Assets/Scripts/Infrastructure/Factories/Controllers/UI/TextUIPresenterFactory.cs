using System;
using Scripts.Controllers.UI;
using Scripts.PresentationInterfaces.UI;
using Scripts.Utils.ObservableProperties.ObservablePropertyInterfaces;

namespace Scripts.Infrastructure.Factories.Controllers.UI
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