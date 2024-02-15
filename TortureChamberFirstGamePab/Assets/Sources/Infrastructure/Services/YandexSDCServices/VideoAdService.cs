using System;
using Agava.WebUtility;
using Sources.Domain.Constants;
using Sources.Domain.Players;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class VideoAdService : IVideoAdService
    {
        private readonly IPauseService _pauseService;
        private readonly IPlayerProvider _playerProvider;

        private PlayerWallet _playerWallet;

        public VideoAdService
        (
            IPauseService pauseService,
            IPlayerProvider playerProvider
        )
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _playerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
        }

        private PlayerWallet PlayerWallet => _playerWallet ??= _playerProvider.PlayerWallet;
        
        //TODO сделать рекламу через какойто таймер и предупреждать перед показом
        //TODO это ревард реклама
        public void Show()
        {
            if(AdBlock.Enabled)
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
    }
}