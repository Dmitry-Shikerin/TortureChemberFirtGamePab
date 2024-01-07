using System;
using MyProject.Sources.Controllers;
using MyProject.Sources.Presentation.Views.Triggers.Wallet;
using Sources.PresentationInterfaces.Views.Items.Coins;
using UnityEngine;

namespace MyProject.Sources.Presentation.Views
{
    public class PlayerWalletView : PresentableView<PlayerWalletPresenter>, IPlayerWalletView
    {
        private WalletTrigger _walletTrigger;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _walletTrigger = GetComponentInChildren<WalletTrigger>() ??
                             throw new NullReferenceException(nameof(WalletTrigger));
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

        private void OnEnter(ICoinAnimationView coinAnimationView)
        {
            Presenter.AddCions(coinAnimationView);
        }

        private void OnExit(ICoinAnimationView coinAnimationView)
        {
            
        }

        public void Add(int quantity)
        {
            Presenter.Add(quantity);
        }

        public void Remove(int quantity)
        {
            Presenter.Remove(quantity);
        }
    }
}