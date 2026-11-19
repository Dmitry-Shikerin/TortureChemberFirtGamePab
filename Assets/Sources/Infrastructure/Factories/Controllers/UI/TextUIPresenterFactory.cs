using System;
using Controllers.UI;
using PresentationInterfaces.UI;
using Utils.ObservableProperties.ObservablePropertyInterfaces;

namespace Infrastructure.Factories.Controllers.UI
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