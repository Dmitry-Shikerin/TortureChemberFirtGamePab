using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Player;
using Sources.Domain.Players;

namespace Sources.Infrastructure.Factories.Controllers.Players
{
    public class PlayerWalletPresenterFactory
    {
        public PlayerWalletPresenter Create(IPlayerWalletView playerWalletView, PlayerWallet playerWallet)
        {
            return new PlayerWalletPresenter(playerWalletView, playerWallet);
        }
    }
}