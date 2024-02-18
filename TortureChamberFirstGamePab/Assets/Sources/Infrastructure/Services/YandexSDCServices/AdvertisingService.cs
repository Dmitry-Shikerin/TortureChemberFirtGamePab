using System;
using Agava.WebUtility;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.Players;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class AdvertisingService : IVideoAdService, IInterstitialAdService
    {
        private readonly IPauseService _pauseService;
        private readonly IPlayerProvider _playerProvider;
        private readonly IWebGlService _webGlService;

        private PlayerWallet _playerWallet;

        public AdvertisingService
        (
            IPauseService pauseService,
            IPlayerProvider playerProvider,
            IWebGlService webGlService
        )
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _playerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
            _webGlService = webGlService ?? throw new ArgumentNullException(nameof(webGlService));
        }

        private PlayerWallet PlayerWallet => _playerWallet ??= _playerProvider.PlayerWallet;
        
        public void ShowVideo()
        {
            if(_webGlService.IsWebGl == false)
                return;
            
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

        //TODo позаниматься скором
        public void ShowInterstitial()
        {
            if(_webGlService.IsWebGl == false)
                return;
            
            if(AdBlock.Enabled)
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