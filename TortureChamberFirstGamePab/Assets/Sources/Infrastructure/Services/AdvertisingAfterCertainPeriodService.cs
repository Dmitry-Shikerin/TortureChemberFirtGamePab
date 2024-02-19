using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.Presentation.Containers.UI.Texts;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class AdvertisingAfterCertainPeriodService : IAdvertisingAfterCertainPeriodService
    {
        private readonly IInterstitialAdService _interstitialAdService;
        private readonly AdvertisingAfterCertainPeriodTextContainer _viewTextContainer;

        private CancellationTokenSource _cancellationTokenSource;

        public AdvertisingAfterCertainPeriodService
        (
            IInterstitialAdService interstitialAdService,
            AdvertisingAfterCertainPeriodTextContainer viewTextContainer
        )
        {
            _interstitialAdService = interstitialAdService ??
                                     throw new ArgumentNullException(nameof(interstitialAdService));
            _viewTextContainer = viewTextContainer
                ? viewTextContainer
                : throw new ArgumentNullException(nameof(viewTextContainer));
        }

        public async void Enter(object payload = null)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            DisableTimer();
            
            await ShowInterstitialAsync(_cancellationTokenSource.Token);
        }

        public void Exit() => 
            _cancellationTokenSource.Cancel();

        private async UniTask ShowInterstitialAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    //TODO потом раскоментировать
                    // await UniTask.Delay(TimeSpan.FromMinutes(Constant.InterstitialService.ShowDelay),
                    //     cancellationToken: _cancellationTokenSource.Token);
                    await UniTask.Delay(TimeSpan.FromSeconds(10),
                        cancellationToken: cancellationToken);

                    EnableTimer();
                    
                    await ShowTimer(cancellationToken);

                    DisableTimer();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private async UniTask ShowTimer(CancellationToken cancellationToken)
        {
            _viewTextContainer.Title.SetText(Constant.AdvertisingTimer.ContentText);
            _viewTextContainer.Timer.SetText("3");

            await UniTask.Delay(TimeSpan.FromSeconds(Constant.AdvertisingTimer.Delay),
                cancellationToken: cancellationToken);
            _viewTextContainer.Timer.SetText("2");

            await UniTask.Delay(TimeSpan.FromSeconds(Constant.AdvertisingTimer.Delay),
                cancellationToken: cancellationToken);
            _viewTextContainer.Timer.SetText("1");

            await UniTask.Delay(TimeSpan.FromSeconds(Constant.AdvertisingTimer.Delay),
                cancellationToken: cancellationToken);

            _interstitialAdService.ShowInterstitial();
        }

        private void DisableTimer()
        {
            _viewTextContainer.Title.Disable();
            _viewTextContainer.Timer.Disable();
        }

        private void EnableTimer()
        {
            _viewTextContainer.Title.Enable();
            _viewTextContainer.Timer.Enable();
        }
    }
}