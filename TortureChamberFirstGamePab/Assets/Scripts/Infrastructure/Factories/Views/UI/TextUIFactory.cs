using System;
using Scripts.Controllers.UI;
using Scripts.Infrastructure.Factories.Controllers.UI;
using Scripts.Presentation.UI.Texts;
using Scripts.PresentationInterfaces.UI;
using Scripts.Utils.ObservableProperties.ObservablePropertyInterfaces;

namespace Scripts.Infrastructure.Factories.Views.UI
{
    public class TextUIFactory
    {
        private readonly TextUIPresenterFactory _textUIPresenterFactory;

        public TextUIFactory(TextUIPresenterFactory textUIPresenterFactory)
        {
            _textUIPresenterFactory = textUIPresenterFactory ??
                                      throw new ArgumentNullException(nameof(textUIPresenterFactory));
        }

        public ITextUI Create(TextUI textUI, IObservableProperty observableProperty)
        {
            if (textUI == null)
                throw new ArgumentNullException(nameof(textUI));

            if (observableProperty == null)
                throw new ArgumentNullException(nameof(observableProperty));

            TextUIPresenter textUIPresenter = _textUIPresenterFactory.Create(textUI, observableProperty);

            textUI.Construct(textUIPresenter);

            return textUI;
        }
    }
}