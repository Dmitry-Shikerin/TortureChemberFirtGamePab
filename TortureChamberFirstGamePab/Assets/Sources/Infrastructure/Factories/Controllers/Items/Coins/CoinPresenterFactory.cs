using System;
using Sources.Controllers.Items.Coins;
using Sources.Domain.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Factories.Controllers.Items.Coins
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