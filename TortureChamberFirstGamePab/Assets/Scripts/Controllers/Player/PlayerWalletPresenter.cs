using System;
using Scripts.Domain.Players;
using Scripts.PresentationInterfaces.Views.Items.Coins;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Controllers.Player
{
    public class PlayerWalletPresenter : PresenterBase
    {
        private readonly PlayerWallet _playerWallet;
        private readonly IPlayerWalletView _playerWalletView;

        public PlayerWalletPresenter(IPlayerWalletView playerWalletView, PlayerWallet playerWallet)
        {
            _playerWalletView = playerWalletView ?? throw new ArgumentNullException(nameof(playerWalletView));
            _playerWallet = playerWallet ?? throw new ArgumentNullException(nameof(playerWallet));
        }

        public void Add(int quantity) =>
            _playerWallet.Add(quantity);

        public void AddCoins(ICoinView coinView)
        {
            coinView.SetPlayerWalletView(_playerWalletView);
            coinView.SetCanMove();
        }
    }
}