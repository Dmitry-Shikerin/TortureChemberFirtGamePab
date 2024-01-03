using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Controllers.UI;
using Sources.PresentationInterfaces.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.UI
{
    public class ImageUI : PresentableView<ImageUIPresenter>, IImageUI
    {
        [SerializeField] private Image _image;

        public float FillAmount => _image.fillAmount;
        
        public void SetSprite(Sprite sprite) => 
            _image.sprite = sprite;

        public void SetFillAmount(float filling) => 
            _image.fillAmount = filling;

        public async UniTask FillMoveTowardsAsync(float fillingRate, CancellationToken cancellationToken)
        {
            await Presenter.FillMoveTowardsAsync(fillingRate, cancellationToken);
        }


        public void SetColor(Color color) => 
            _image.color = color;

        public void HideImage() => 
            SetColor(Color.clear);

        public void ShowImage() => 
            SetColor(Color.white);
    }
}