using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Presentation.UI;

namespace Sources.Controllers.UI
{
    public class ButtonUIPresenter : PresenterBase
    {
        private readonly IButtonUI _buttonUI;
        private readonly Action _action;

        public ButtonUIPresenter(IButtonUI buttonUI ,Action action)
        {
            _buttonUI = buttonUI ?? throw new ArgumentNullException(nameof(buttonUI));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void OnClick()
        {
            _action.Invoke();
        }
    }
}