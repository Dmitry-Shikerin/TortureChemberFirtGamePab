using System;
using Controllers.Player;
using Presentation.Triggers.Wallet;
using PresentationInterfaces.Views.Items.Coins;
using PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Presentation.Views.Player
{
    public class PlayerWalletView : PresentableView<PlayerWalletPresenter>, IPlayerWalletView
    {
        private WalletTrigger _walletTrigger;

        public Vector3 Position => transform.position;

        private void Awake() =>
            _walletTrigger = GetComponent<WalletTrigger>() ??
                             throw new NullReferenceException(nameof(WalletTrigger));

        public void Add(int quantity) =>
            Presenter.Add(quantity);

        protected override void OnAfterEnable()
        {
            _walletTrigger.Entered += OnEnter;
            _walletTrigger.Exited += OnExit;
        }

        protected override void OnAfterDisable()
        {
            _walletTrigger.Entered -= OnEnter;
            _walletTrigger.Exited -= OnExit;
        }

        private void OnEnter(ICoinView coinView) =>
            Presenter.AddCoins(coinView);

        private void OnExit(ICoinView coinView)
        {
        }
    }
}