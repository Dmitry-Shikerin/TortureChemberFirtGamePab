using Sources.PresentationInterfaces.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.UI
{
    //TODO не придумал как засунуть инфу в презентер этой вьюшки
    public class ImageUI : MonoBehaviour, IImageUI
    {
        [SerializeField] private Image _image;

        public float FillAmount => _image.fillAmount;
        
        public void SetSprite(Sprite sprite) => 
            _image.sprite = sprite;

        public void SetFillAmount(float filling) => 
            _image.fillAmount = filling;

        public void SetColor(Color color) => 
            _image.color = color;

        public void Hide() => 
            SetColor(Color.clear);

        public void Show() => 
            SetColor(Color.white);
    }
}