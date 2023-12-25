using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Domain.Constants;
using Sources.PresentationInterfaces.UI;
using UnityEngine;

namespace Sources.Controllers.UI
{
    public class ImageUIPresenter : PresenterBase
    {
        private const float MinValue = 0;
        private const float MaxValue = 1;
        
        private readonly IImageUI _imageUI;

        public ImageUIPresenter(IImageUI imageUI)
        {
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
        }

        public async UniTask FillMoveTowardsAsync(float fillingRate, CancellationToken cancellationToken)
        {
            _imageUI.SetFillAmount(MaxValue);

            while (_imageUI.FillAmount > Constant.Epsilon)
            {
                float fill = Mathf.MoveTowards(
                    _imageUI.FillAmount, MinValue,
                    fillingRate * Time.deltaTime);
                _imageUI.SetFillAmount(fill);

                await UniTask.Yield(cancellationToken);
            }

            _imageUI.SetFillAmount(MinValue);
        }
    }
}