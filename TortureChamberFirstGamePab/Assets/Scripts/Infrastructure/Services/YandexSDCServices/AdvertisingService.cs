using System;
using Agava.WebUtility;
using Agava.YandexGames;
using Scripts.Domain.Constants;
using Scripts.Domain.Players;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using Scripts.InfrastructureInterfaces.Services.Providers.Players;
using Scripts.InfrastructureInterfaces.Services.SDCServices;

namespace Scripts.Infrastructure.Services.YandexSDCServices
{
    public class AdvertisingService : IVideoAdService, IInterstitialAdService
    {
        private readonly IPauseService _pauseService;
        private readonly IPlayerProvider _playerProvider;

        private PlayerWallet _playerWallet;

        public AdvertisingService(
            IPauseService pauseService,
            IPlayerProvider playerProvider)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _playerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
        }

        private PlayerWallet PlayerWallet => _playerWallet ??= _playerProvider.PlayerWallet;

        public void ShowInterstitial()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (AdBlock.Enabled)
                return;

            InterstitialAd.Show(
                () =>
                {
                    _pauseService.Pause();
                    _pauseService.PauseSound();
                },
                _ =>
                {
                    _pauseService.Continue();
                    _pauseService.ContinueSound();
                });
        }

        public void ShowVideo(Action onCloseCallback)
        {
            if (WebApplication.IsRunningOnWebGL == false)
            {
                onCloseCallback?.Invoke();

                return;
            }

            if (AdBlock.Enabled)
            {
                onCloseCallback?.Invoke();

                return;
            }

            VideoAd.Show(
                () =>
                {
                    _pauseService.Pause();
                    _pauseService.PauseSound();
                },
                () =>
                    PlayerWallet.Add(AdvertisingConstant.CoinsAmount),
                () =>
                {
                    _pauseService.Continue();
                    _pauseService.ContinueSound();

                    onCloseCallback?.Invoke();
                });
        }
    }
}