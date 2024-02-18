using Sources.Controllers.UI;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.UI;
using TMPro;
using UnityEngine;

namespace Sources.Presentation.UI
{
    public class TextUI : PresentableView<TextUIPresenter>, ITextUI
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public virtual void SetText(string text) => 
            _text.text = text;
    }
}