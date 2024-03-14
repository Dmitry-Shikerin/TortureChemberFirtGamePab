using System;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.UI;

namespace Sources.Infrastructure.Factories.Views.UI
{
    public class ButtonUIFactory
    {
        private readonly ButtonUIPresenterFactory _buttonUIPresenterFactory;

        public ButtonUIFactory(ButtonUIPresenterFactory buttonUIPresenterFactory)
        {
            _buttonUIPresenterFactory = buttonUIPresenterFactory ??
                                        throw new ArgumentNullException(nameof(buttonUIPresenterFactory));
        }

        public IButtonUI Create(ButtonUI buttonUI, Action action)
        {
            if (buttonUI == null)
                throw new ArgumentNullException(nameof(buttonUI));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var buttonUIPresenter = _buttonUIPresenterFactory.Create(buttonUI, action);
            buttonUI.Construct(buttonUIPresenter);

            return buttonUI;
        }
    }
}