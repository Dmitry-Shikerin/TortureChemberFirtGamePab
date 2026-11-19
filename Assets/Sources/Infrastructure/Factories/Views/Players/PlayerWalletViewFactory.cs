using System;
using Controllers.Player;
using Domain.Players;
using Infrastructure.Factories.Controllers.Players;
using Presentation.Views.Player;

namespace Infrastructure.Factories.Views.Players
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