using TMPro;
using UnityEngine;

namespace Sources.Presentation.UI.Texts
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        //TODO добавить возможность прятать текст
        public virtual void SetText(string text) => 
            _text.text = text;

        public void Enable() => 
            _text.enabled = true;
        
        public void Disable() => 
            _text.enabled = false;
    }
}