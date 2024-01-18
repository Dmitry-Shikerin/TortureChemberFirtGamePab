using System;
using Sources.Controllers.UI;
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
            ButtonUIPresenter buttonUIPresenter = _buttonUIPresenterFactory.Create(buttonUI, action);
            buttonUI.Construct(buttonUIPresenter);

            return buttonUI;
        }
    }
}