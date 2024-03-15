using Scripts.Presentation.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Presentation.UI
{
    public class ImageView : View
    {
        [SerializeField] private Image _image;

        public void SetSprite(Sprite sprite) =>
            _image.sprite = sprite;
    }
}