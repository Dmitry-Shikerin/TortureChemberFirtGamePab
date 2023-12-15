using UnityEngine;

namespace Sources.PresentationInterfaces.UI
{
    public interface IImageUI
    {
        void SetSprite(Sprite sprite);
        void SetFilling(float filling);
        void SetColor(Color color);
    }
}