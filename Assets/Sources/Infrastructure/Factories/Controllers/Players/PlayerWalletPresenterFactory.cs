using System;
using Controllers.Player;
using Domain.Players;
using PresentationInterfaces.Views.Players;

namespace Infrastructure.Factories.Controllers.Players
{
    public class PlayerWalletPresenterFactory
    {
        public PlayerWalletPresenter Create(IPlayerWalletView playerWalletView, PlayerWallet playerWallet)
        {
            if (playerWalletView == null)
                throw new ArgumentNullException(nameof(playerWalletView));

            if (playerWallet == null)
                throw new ArgumentNullException(nameof(playerWallet));

            return new PlayerWalletPresenter(playerWalletView, playerWallet);
        }
    }
}