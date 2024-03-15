using System;
using Scripts.PresentationInterfaces.UI.Buttons;

namespace Scripts.Controllers.UI
{
    public class ButtonUIPresenter : PresenterBase
    {
        private readonly Action _clickAction;
        private readonly IButtonUI _buttonUI;

        public ButtonUIPresenter(
            IButtonUI buttonUI,
            Action clickAction)
        {
            _buttonUI = buttonUI ?? throw new ArgumentNullException(nameof(buttonUI));
            _clickAction = clickAction ?? throw new ArgumentNullException(nameof(clickAction));
        }

        public override void Enable() =>
            _buttonUI.AddListener(OnClick);

        public override void Disable() =>
            _buttonUI.RemoveListener(OnClick);

        private void OnClick() =>
            _clickAction.Invoke();
    }
}