using System;
using Controllers.UI;
using PresentationInterfaces.UI.Buttons;

namespace Infrastructure.Factories.Controllers.UI
{
    public class ButtonUIPresenterFactory
    {
        public ButtonUIPresenter Create(IButtonUI buttonUI, Action action)
        {
            if (buttonUI == null)
                throw new ArgumentNullException(nameof(buttonUI));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            return new ButtonUIPresenter(buttonUI, action);
        }
    }
}