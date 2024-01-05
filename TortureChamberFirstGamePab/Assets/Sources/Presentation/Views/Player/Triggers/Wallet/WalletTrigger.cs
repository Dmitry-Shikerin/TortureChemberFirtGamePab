using System;
using Sources.Presentation.Views.Items.Coins;
using UnityEngine;

namespace MyProject.Sources.Presentation.Views.Triggers.Wallet
{
    public class WalletTrigger : MonoBehaviour
    {
        private PlayerWalletView _playerWalletView;
        
        private void Awake()
        {
            _playerWalletView = GetComponentInParent<PlayerWalletView>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CoinAnimationView coinAnimationView))
            {
                coinAnimationView.SetPlayerWalletView(_playerWalletView);
                coinAnimationView.SetCanMove(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
        }
    }
}