using System;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;

namespace Sources.Infrastructure.Factories.Controllers.Players
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