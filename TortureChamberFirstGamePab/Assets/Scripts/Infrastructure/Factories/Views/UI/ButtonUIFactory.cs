using System;
using Scripts.Controllers.UI;
using Scripts.Infrastructure.Factories.Controllers.UI;
using Scripts.Presentation.UI.Buttons;
using Scripts.PresentationInterfaces.UI.Buttons;

namespace Scripts.Infrastructure.Factories.Views.UI
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

            ButtonUIPresenter buttonUIPresenter = _buttonUIPresenterFactory.Create(buttonUI, action);
            buttonUI.Construct(buttonUIPresenter);

            return buttonUI;
        }
    }
}