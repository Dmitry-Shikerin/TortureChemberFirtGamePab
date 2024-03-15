using System;
using Scripts.Controllers.Player;
using Scripts.Domain.Players;
using Scripts.Infrastructure.Factories.Controllers.Players;
using Scripts.Presentation.Views.Player;

namespace Scripts.Infrastructure.Factories.Views.Players
{
    public class PlayerWalletViewFactory
    {
        private readonly PlayerWalletPresenterFactory _playerWalletPresenterFactory;

        public PlayerWalletViewFactory(PlayerWalletPresenterFactory playerWalletPresenterFactory)
        {
            _playerWalletPresenterFactory = playerWalletPresenterFactory ??
                                            throw new ArgumentNullException(nameof(playerWalletPresenterFactory));
        }

        public PlayerWalletView Create(PlayerWallet playerWallet, PlayerWalletView playerWalletView)
        {
            PlayerWalletPresenter playerWalletPresenter =
                _playerWalletPresenterFactory.Create(playerWalletView, playerWallet);

            playerWalletView.Construct(playerWalletPresenter);

            return playerWalletView;
        }
    }
}