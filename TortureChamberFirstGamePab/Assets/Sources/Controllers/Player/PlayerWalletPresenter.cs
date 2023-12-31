﻿using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using MyProject.Sources.Presentation.Views;
using Sources.Domain.Players;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace MyProject.Sources.Controllers
{
    public class PlayerWalletPresenter : PresenterBase
    {
        private readonly IPlayerWalletView _playerWalletView;
        private readonly PlayerWallet _playerWallet;

        public PlayerWalletPresenter(IPlayerWalletView playerWalletView, PlayerWallet playerWallet)
        {
            _playerWalletView = playerWalletView ?? throw new ArgumentNullException(nameof(playerWalletView));
            _playerWallet = playerWallet ?? throw new ArgumentNullException(nameof(playerWallet));
        }


        public void Add(int quantity)
        {
            _playerWallet.Add(quantity);
        }

        public void Remove(int quantity)
        {
            _playerWallet.Remove(quantity);
        }

        public void AddCions(ICoinAnimationView coinAnimationView)
        {
            coinAnimationView.SetPlayerWalletView(_playerWalletView);
            coinAnimationView.SetCanMove(true);
        }
    }
}