using UnityEngine;

namespace Sources.PresentationInterfaces.UI
{
    public interface IImageUI
    {
        float FillAmount { get; }
        
        void SetSprite(Sprite sprite);
        void SetFillAmount(float filling);
        void SetColor(Color color);
        void Hide();
        void Show();
    }
}