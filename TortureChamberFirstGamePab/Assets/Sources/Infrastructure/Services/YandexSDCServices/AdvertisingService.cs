using System;
using Agava.WebUtility;
using Sources.Domain.Constants;
using Sources.Domain.Players;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.Providers.Players;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class AdvertisingService : IVideoAdService, IInterstitialAdService
    {
        private readonly IPauseService _pauseService;
        private readonly IPlayerProvider _playerProvider;

        private PlayerWallet _playerWallet;

        public AdvertisingService
        (
            IPauseService pauseService,
            IPlayerProvider playerProvider
        )
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _playerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
        }

        private PlayerWallet PlayerWallet => _playerWallet ??= _playerProvider.PlayerWallet;
        
        public void ShowVideo()
        {
            //TODO указать в обучении что ревард реклама не запустится еесли стоит АддБлок 
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (AdBlock.Enabled)
                return;

            Agava.YandexGames.VideoAd.Show(
                () =>
                {
                    _pauseService.Pause();
                    _pauseService.PauseSound();
                },
                () =>
                    PlayerWallet.Add(Constant.AdvertisingReward.CoinsAmount), 
                () =>
                {
                    _pauseService.Continue();
                    _pauseService.ContinueSound();
                });
        }

        public void ShowInterstitial()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (AdBlock.Enabled)
                return;

            Agava.YandexGames.InterstitialAd.Show(
                () =>
                {
                    _pauseService.Pause();
                    _pauseService.PauseSound();
                },
                 (isOpen) =>
                {
                    _pauseService.Continue();
                    _pauseService.ContinueSound();
                });
        }
    }
}