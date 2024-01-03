using System;
using Sources.Controllers.UI;
using Sources.Presentation.UI;

namespace Sources.Infrastructure.Factories.Controllers.UI
{
    public class ButtonUIPresenterFactory
    {
        public ButtonUIPresenter Create(IButtonUI buttonUI ,Action action)
        {
            return new ButtonUIPresenter(buttonUI ,action);
        }
    }
}