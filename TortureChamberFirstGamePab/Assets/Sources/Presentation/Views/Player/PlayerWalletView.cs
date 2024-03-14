using System;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Player;
using Sources.Presentation.Triggers.Wallet;
using Sources.PresentationInterfaces.Views.Items.Coins;
using UnityEngine;

namespace Sources.Presentation.Views.Player
{
    public class PlayerWalletView : PresentableView<PlayerWalletPresenter>, IPlayerWalletView
    {
        private WalletTrigger _walletTrigger;

        private void Awake()
        {
            _walletTrigger = GetComponent<WalletTrigger>() ??
                             throw new NullReferenceException(nameof(WalletTrigger));
        }

        public Vector3 Position => transform.position;

        public void Add(int quantity)
        {
            Presenter.Add(quantity);
        }

        public void Remove(int quantity)
        {
            Presenter.Remove(quantity);
        }

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

        private void OnEnter(ICoinView coinView)
        {
            Presenter.AddCoins(coinView);
        }

        private void OnExit(ICoinView coinView)
        {
        }
    }
}