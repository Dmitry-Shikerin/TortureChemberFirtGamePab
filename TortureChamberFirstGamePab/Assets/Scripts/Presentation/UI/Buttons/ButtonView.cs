using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.UI.Buttons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Presentation.UI.Buttons
{
    public class ButtonView : View, IButtonView
    {
        [SerializeField] private Button _button;

        public void AddClickListener(UnityAction onClick) =>
            _button.onClick.AddListener(onClick);

        public void RemoveClickListener(UnityAction onClick) =>
            _button.onClick.RemoveListener(onClick);
    }
}