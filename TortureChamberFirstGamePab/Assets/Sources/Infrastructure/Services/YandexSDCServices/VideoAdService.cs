﻿using System;
using Sources.Domain.Constants;
using Sources.Domain.Players;
using Sources.Infrastructure.Services.Providers.Players;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class VideoAdService
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
        
        public void Show() =>
            Agava.YandexGames.VideoAd.Show(_pauseService.Pause,
                OnRewardCallback, _pauseService.Continue);

        private void OnRewardCallback() => 
            PlayerWallet.Add(Constant.AdvertisingReward.CoinsAmount);
    }
}