using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.UI;
using UnityEngine;

namespace Sources.Controllers.UI
{
    public class ImageUIPresenter : PresenterBase
    {
        private readonly IImageUI _imageUI;
        private readonly IPauseService _pauseService;

        public ImageUIPresenter
        (
            IImageUI imageUI,
            IPauseService pauseService
        )
        {
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public async UniTask FillMoveTowardsAsync(float fillingRate, 
            CancellationToken cancellationToken, Action action)
        {
            _imageUI.SetFillAmount(Constant.FillingAmount.Maximum);
            
            while (_imageUI.FillAmount > Constant.Epsilon)
            {
                float fill = Mathf.MoveTowards(_imageUI.FillAmount,
                    Constant.FillingAmount.Minimum, fillingRate * Time.deltaTime);

                _imageUI.SetFillAmount(fill);

                action.Invoke();
                
                //TODO сделать поп аналогии
                await _pauseService.Yield(cancellationToken);
            }

            _imageUI.SetFillAmount(Constant.FillingAmount.Minimum);
        }
        
        public async UniTask FillMoveTowardsAsync(float fillingRate, 
            CancellationToken cancellationToken)
        {
            _imageUI.SetFillAmount(Constant.FillingAmount.Maximum);

            while (_imageUI.FillAmount > Constant.Epsilon)
            {
                float fill = Mathf.MoveTowards(_imageUI.FillAmount,
                    Constant.FillingAmount.Minimum, fillingRate * Time.deltaTime);

                _imageUI.SetFillAmount(fill);
                
                await _pauseService.Yield(cancellationToken);
            }

            _imageUI.SetFillAmount(Constant.FillingAmount.Minimum);
        }
    }
}