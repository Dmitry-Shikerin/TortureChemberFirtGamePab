using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.PresentationInterfaces.UI;
using UnityEngine;

namespace Sources.Controllers.UI
{
    public class ImageUIPresenter : PresenterBase
    {
        private readonly IImageUI _imageUI;

        public ImageUIPresenter(IImageUI imageUI)
        {
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
        }

        public async UniTask FillMoveTowardsAsync(float fillingRate, CancellationToken cancellationToken)
        {
            _imageUI.SetFillAmount(Constant.FillingAmount.Maximum);

            while (_imageUI.FillAmount > Constant.Epsilon)
            {
                float fill = Mathf.MoveTowards(_imageUI.FillAmount, 
                    Constant.FillingAmount.Minimum, fillingRate * Time.deltaTime);
                
                _imageUI.SetFillAmount(fill);

                await UniTask.Yield(cancellationToken);
                await Pause();
            }

            _imageUI.SetFillAmount(Constant.FillingAmount.Minimum);
        }

        //TODO это будет в UPdateService
        private async UniTask Pause()
        {
            //TODO это из другого сервиси и это поле
            bool isPaused = false;

            while (isPaused)
                await UniTask.Yield();
        }
    }
}