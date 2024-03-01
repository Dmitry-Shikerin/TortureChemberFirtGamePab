using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.Presentation.Containers.UI.Texts;

namespace Sources.Infrastructure.Services
{
    public class AdvertisingAfterCertainPeriodService : IAdvertisingAfterCertainPeriodService
    {
        private readonly IInterstitialAdService _interstitialAdService;
        private readonly AdvertisingAfterCertainPeriodViewContainer _viewContainer;

        private CancellationTokenSource _cancellationTokenSource;
        private TimeSpan _advertisementTimeSpan;
        private TimeSpan _timerTimeSpan;
        
        public AdvertisingAfterCertainPeriodService
        (
            IInterstitialAdService interstitialAdService,
            AdvertisingAfterCertainPeriodViewContainer viewViewContainer
        )
        {
            _interstitialAdService = interstitialAdService ??
                                     throw new ArgumentNullException(nameof(interstitialAdService));
            _viewContainer = viewViewContainer
                ? viewViewContainer
                : throw new ArgumentNullException(nameof(viewViewContainer));
        }

        public async void Enter(object payload = null)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _advertisementTimeSpan = TimeSpan.FromMinutes(Constant.InterstitialService.ShowDelay);
            _timerTimeSpan = TimeSpan.FromSeconds(Constant.AdvertisingTimer.Delay);

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
                    await UniTask.Delay(_advertisementTimeSpan,
                        cancellationToken: cancellationToken);

                    EnableTimer();
                    
                    await ShowTimerAsync(cancellationToken);

                    DisableTimer();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private async UniTask ShowTimerAsync(CancellationToken cancellationToken)
        {
            _viewContainer.Title.SetText(Constant.AdvertisingTimer.ContentText);
            _viewContainer.Timer.SetText("3");

            await UniTask.Delay(_timerTimeSpan,
                cancellationToken: cancellationToken);
            _viewContainer.Timer.SetText("2");

            await UniTask.Delay(_timerTimeSpan,
                cancellationToken: cancellationToken);
            _viewContainer.Timer.SetText("1");

            await UniTask.Delay(_timerTimeSpan,
                cancellationToken: cancellationToken);

            _interstitialAdService.ShowInterstitial();
        }

        private void DisableTimer()
        {
            _viewContainer.Hide();
            
            _viewContainer.Title.Disable();
            _viewContainer.Timer.Disable();
        }

        private void EnableTimer()
        {
            _viewContainer.Show();
            
            _viewContainer.Title.Enable();
            _viewContainer.Timer.Enable();
        }
    }
}