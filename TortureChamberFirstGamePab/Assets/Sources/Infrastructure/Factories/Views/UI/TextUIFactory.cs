﻿using System;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.UI;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;

namespace Sources.Infrastructure.Factories.Views.UI
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

            var textUIPresenter = _textUIPresenterFactory.Create(textUI, observableProperty);

            textUI.Construct(textUIPresenter);

            return textUI;
        }
    }
}