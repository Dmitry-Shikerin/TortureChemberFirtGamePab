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

        private bool _isPlaying;

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
            
            await ShowInterstitialAsync();
        }

        public void Exit()
        {
            _isPlaying = false;

            _cancellationTokenSource.Cancel();
        }

        private async UniTask ShowInterstitialAsync()
        {
            _isPlaying = true;

            Debug.Log("AdvertisingService start");

            //TODO как избавится от этого трай кетча?
            try
            {
                while (_isPlaying)
                {
                    //TODO потом раскоментировать
                    // await UniTask.Delay(TimeSpan.FromMinutes(Constant.InterstitialService.ShowDelay),
                    //     cancellationToken: _cancellationTokenSource.Token);
                    await UniTask.Delay(TimeSpan.FromSeconds(10),
                        cancellationToken: _cancellationTokenSource.Token);

                    EnableTimer();
                    
                    await ShowTimer();

                    DisableTimer();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private async UniTask ShowTimer()
        {
            _viewTextContainer.Title.SetText(Constant.AdvertisingTimer.ContentText);
            _viewTextContainer.Timer.SetText("3");

            await UniTask.Delay(TimeSpan.FromSeconds(Constant.AdvertisingTimer.Delay),
                cancellationToken: _cancellationTokenSource.Token);
            _viewTextContainer.Timer.SetText("2");

            await UniTask.Delay(TimeSpan.FromSeconds(Constant.AdvertisingTimer.Delay),
                cancellationToken: _cancellationTokenSource.Token);
            _viewTextContainer.Timer.SetText("1");

            await UniTask.Delay(TimeSpan.FromSeconds(Constant.AdvertisingTimer.Delay),
                cancellationToken: _cancellationTokenSource.Token);

            Debug.Log("Show Advertising");

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