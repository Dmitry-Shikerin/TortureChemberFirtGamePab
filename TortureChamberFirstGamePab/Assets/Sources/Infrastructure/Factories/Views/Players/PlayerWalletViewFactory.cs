using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers;
using MyProject.Sources.Infrastructure.Factorys.Controllers;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;
using Sources.Presentation.Views.Player;

namespace MyProject.Sources.Infrastructure.Factorys.Views
{
    public class PlayerWalletViewFactory
    {
        private readonly PlayerWalletPresenterFactory _playerWalletPresenterFactory;

        public PlayerWalletViewFactory(PlayerWalletPresenterFactory playerWalletPresenterFactory)
        {
            _playerWalletPresenterFactory = playerWalletPresenterFactory ??
                                            throw new ArgumentNullException(nameof(playerWalletPresenterFactory));
        }

        public IPlayerWalletView Create(PlayerWalletView playerWalletView, PlayerWallet playerWallet)
        {
            PlayerWalletPresenter playerWalletPresenter = 
                _playerWalletPresenterFactory.Create(playerWalletView, playerWallet);
            
            playerWalletView.Construct(playerWalletPresenter);

            return playerWalletView;
        }
    }
}