using System;
using Scripts.Controllers.Player;
using Scripts.Domain.Players;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Infrastructure.Factories.Controllers.Players
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