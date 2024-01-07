using MyProject.Sources.Presentation.Views;
using Sources.Domain.Players;

namespace Sources.Domain.Items.Coins
{
    public class CoinAnimation
    {
        public IPlayerWalletView PlayerWalletView { get; private set; }
        public bool CanMove { get; private set; }

        public void SetCanMove(bool canMove)
        {
            CanMove = canMove;
        }

        public void SetPlayerWalletView(IPlayerWalletView playerWalletView)
        {
            PlayerWalletView = playerWalletView;
        }
    }
}