using Sources.Presentation.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.UI
{
    public class ImageView : View
    {
        [SerializeField] private Image _image;

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}