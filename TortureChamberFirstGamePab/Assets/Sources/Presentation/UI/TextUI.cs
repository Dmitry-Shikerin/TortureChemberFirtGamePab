using MyProject.Sources.Controllers.UI;
using Sources.PresentationInterfaces.UI;
using TMPro;
using UnityEngine;

namespace Sources.Presentation.UI
{
    public class TextUI : MonoBehaviour, ITextUI
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public virtual void SetText(string text) => 
            _text.text = text;
    }
}