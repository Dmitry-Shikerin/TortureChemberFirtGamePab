using System;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Controllers.Players;
using Sources.Presentation.Views.Player;

namespace Sources.Infrastructure.Factories.Views.Players
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