using System;
using Controllers.UI;
using Infrastructure.Factories.Controllers.UI;
using Presentation.UI.Texts;
using PresentationInterfaces.UI;
using Utils.ObservableProperties.ObservablePropertyInterfaces;

namespace Infrastructure.Factories.Views.UI
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