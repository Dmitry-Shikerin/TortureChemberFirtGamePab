using Controllers.UI;
using Presentation.Views;
using PresentationInterfaces.UI;
using TMPro;
using UnityEngine;

namespace Presentation.UI.Texts
{
    public class TextUI : PresentableView<TextUIPresenter>, ITextUI
    {
        [SerializeField] private TextMeshProUGUI _text;

        public virtual void SetText(string text) =>
            _text.text = text;
    }
}