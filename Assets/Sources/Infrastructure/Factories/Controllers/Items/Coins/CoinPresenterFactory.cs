using System;
using Controllers.Items.Coins;
using Domain.Items.Coins;
using PresentationInterfaces.Views.Items.Coins;

namespace Infrastructure.Factories.Controllers.Items.Coins
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