using Scripts.Controllers.UI;
using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.UI.Buttons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Presentation.UI.Buttons
{
    public class ButtonUI : PresentableView<ButtonUIPresenter>, IButtonUI
    {
        [SerializeField] private Button _button;

        public void AddListener(UnityAction action) =>
            _button.onClick.AddListener(action);

        public void RemoveListener(UnityAction action) =>
            _button.onClick.RemoveListener(action);

        public void Enable() =>
            _button.enabled = true;

        public void Disable() =>
            _button.enabled = false;
    }
}