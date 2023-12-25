using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.PresentationInterfaces.UI
{
    public interface IImageUI
    {
        float FillAmount { get; }
        
        void SetSprite(Sprite sprite);
        void SetFillAmount(float filling);
        UniTask FillMoveTowardsAsync(float fillingRate, CancellationToken cancellationToken);
        void SetColor(Color color);
        void Hide();
        void Show();
    }
}