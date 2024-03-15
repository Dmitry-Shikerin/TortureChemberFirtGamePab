using System;
using Scripts.Controllers.Items.Coins;
using Scripts.Domain.Items.Coins;
using Scripts.PresentationInterfaces.Views.Items.Coins;

namespace Scripts.Infrastructure.Factories.Controllers.Items.Coins
{
    public class CoinPresenterFactory
    {
        public CoinPresenter Create(ICoinView coinView, Coin coin)
        {
            if (coinView == null)
                throw new ArgumentNullException(nameof(coinView));

            if (coin == null)
                throw new ArgumentNullException(nameof(coin));

            return new CoinPresenter(coinView, coin);
        }
    }
}