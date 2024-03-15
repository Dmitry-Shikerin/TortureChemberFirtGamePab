using Scripts.Controllers.UI;
using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.UI;
using TMPro;
using UnityEngine;

namespace Scripts.Presentation.UI.Texts
{
    public class TextUI : PresentableView<TextUIPresenter>, ITextUI
    {
        [SerializeField] private TextMeshProUGUI _text;

        public virtual void SetText(string text) =>
            _text.text = text;
    }
}