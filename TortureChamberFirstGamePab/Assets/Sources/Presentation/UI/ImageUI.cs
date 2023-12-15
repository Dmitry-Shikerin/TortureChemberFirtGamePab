using Sources.PresentationInterfaces.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.UI
{
    //TODO не придумал как засунуть инфу в презентер этой вьюшки
    public class ImageUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void SetFilling(float filling)
        {
            _image.fillAmount = filling;
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }
    }
}