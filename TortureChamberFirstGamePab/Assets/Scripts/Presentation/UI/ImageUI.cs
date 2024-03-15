using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Controllers.UI;
using Scripts.Domain.Constants;
using Scripts.Presentation.Views;
using Scripts.PresentationInterfaces.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Presentation.UI
{
    public class ImageUI : PresentableView<ImageUIPresenter>, IImageUI
    {
        [SerializeField] private Image _image;

        public float FillAmount => _image.fillAmount;

        public void SetSprite(Sprite sprite) =>
            _image.sprite = sprite;

        public void SetFillAmount(float filling) =>
            _image.fillAmount = filling;

        public async UniTask FillMoveTowardsAsync(
            float fillingRate, CancellationToken cancellationToken, Action action) =>
            await Presenter.FillMoveTowardsAsync(fillingRate, cancellationToken, action);

        public async UniTask FillMoveTowardsAsync(float fillingRate, CancellationToken cancellationToken) =>
            await Presenter.FillMoveTowardsAsync(fillingRate, cancellationToken);

        public void SetColor(Color color) =>
            _image.color = color;

        public void HideImage() =>
            _image.fillAmount = FillingAmountConstant.Minimum;

        public void ShowImage() =>
            _image.fillAmount = FillingAmountConstant.Maximum;
    }
}