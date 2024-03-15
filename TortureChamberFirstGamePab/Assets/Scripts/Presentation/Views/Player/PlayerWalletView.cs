using System;
using Scripts.Controllers.Player;
using Scripts.Presentation.Triggers.Wallet;
using Scripts.PresentationInterfaces.Views.Items.Coins;
using Scripts.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Scripts.Presentation.Views.Player
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