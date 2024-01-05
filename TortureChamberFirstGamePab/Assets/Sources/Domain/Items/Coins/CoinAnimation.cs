using MyProject.Sources.Presentation.Views;
using Sources.Domain.Players;

namespace Sources.Domain.Items.Coins
{
    public class CoinAnimation
    {
        public PlayerWalletView PlayerWalletView { get; private set; }
        public bool CanMove { get; private set; }

        public void SetCanMove(bool canMove)
        {
            CanMove = canMove;
        }

        public void SetPlayerWalletView(PlayerWalletView playerWalletView)
        {
            PlayerWalletView = playerWalletView;
        }
    }
}