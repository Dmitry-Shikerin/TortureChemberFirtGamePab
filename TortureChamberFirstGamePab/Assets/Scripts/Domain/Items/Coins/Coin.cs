using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Domain.Items.Coins
{
    public class Coin
    {
        public IPlayerWalletView PlayerWalletView { get; private set; }
        public bool CanMove { get; private set; }
        public int CoinAmount { get; private set; }

        public void SetCoinAmount(int amount)
        {
            if (amount <= 0)
                return;

            CoinAmount = amount;
        }

        public void SetCanMove()
        {
            CanMove = true;
        }

        public void SetPlayerWalletView(IPlayerWalletView playerWalletView)
        {
            PlayerWalletView = playerWalletView;
        }
    }
}