using System;
using Sources.Controllers.Items.Coins;
using Sources.Domain.Items.Coins;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Factories.Controllers.Items.Coins
{
    public class CoinPresenterFactory
    {
        private readonly IPauseService _pauseService;

        public CoinPresenterFactory(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public CoinPresenter Create(ICoinView coinView, Coin coin)
        {
            if (coinView == null) 
                throw new ArgumentNullException(nameof(coinView));
            
            if (coin == null) 
                throw new ArgumentNullException(nameof(coin));
            
            return new CoinPresenter(coinView, coin, _pauseService);
        }
    }
}