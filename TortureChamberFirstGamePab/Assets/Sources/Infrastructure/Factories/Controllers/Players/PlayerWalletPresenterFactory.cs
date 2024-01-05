using MyProject.Sources.Controllers;
using MyProject.Sources.Presentation.Views;
using Sources.Domain.Players;

namespace MyProject.Sources.Infrastructure.Factorys.Controllers
{
    public class PlayerWalletPresenterFactory
    {
        public PlayerWalletPresenter Create(IPlayerWalletView playerWalletView, PlayerWallet playerWallet)
        {
            return new PlayerWalletPresenter(playerWalletView, playerWallet);
        }
    }
}