using Sources.Presentation.Views;
using Sources.PresentationInterfaces.UI.Buttons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.Presentation.UI.Buttons
{
    public class ButtonView : View, IButtonView
    {
        [SerializeField] private Button _button;

        public void AddClickListener(UnityAction onClick)
        {
            _button.onClick.AddListener(onClick);
        }

        public void RemoveClickListener(UnityAction onClick)
        {
            _button.onClick.RemoveListener(onClick);
        }
    }
}