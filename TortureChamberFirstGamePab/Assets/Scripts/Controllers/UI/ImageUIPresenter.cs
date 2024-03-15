using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Domain.Constants;
using Scripts.PresentationInterfaces.UI;
using UnityEngine;

namespace Scripts.Controllers.UI
{
    public class ImageUIPresenter : PresenterBase
    {
        private readonly IImageUI _imageUI;

        public ImageUIPresenter(IImageUI imageUI)
        {
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
        }

        public async UniTask FillMoveTowardsAsync(
            float fillingRate,
            CancellationToken cancellationToken,
            Action action)
        {
            _imageUI.SetFillAmount(FillingAmountConstant.Maximum);

            while (_imageUI.FillAmount > MathfConstant.Epsilon)
            {
                var fill = Mathf.MoveTowards(
                    _imageUI.FillAmount,
                    FillingAmountConstant.Minimum,
                    fillingRate * Time.deltaTime);

                _imageUI.SetFillAmount(fill);

                action.Invoke();

                await UniTask.Yield(cancellationToken);
            }

            _imageUI.SetFillAmount(FillingAmountConstant.Minimum);
        }

        public async UniTask FillMoveTowardsAsync(
            float fillingRate,
            CancellationToken cancellationToken)
        {
            _imageUI.SetFillAmount(FillingAmountConstant.Maximum);

            while (_imageUI.FillAmount > MathfConstant.Epsilon)
            {
                var fill = Mathf.MoveTowards(
                    _imageUI.FillAmount,
                    FillingAmountConstant.Minimum,
                    fillingRate * Time.deltaTime);

                _imageUI.SetFillAmount(fill);

                await UniTask.Yield(cancellationToken);
            }

            _imageUI.SetFillAmount(FillingAmountConstant.Minimum);
        }
    }
}