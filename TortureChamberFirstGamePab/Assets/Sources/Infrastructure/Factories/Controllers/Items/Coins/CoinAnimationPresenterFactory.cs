using System;
using Sources.Controllers.Items.Coins;
using Sources.Domain.Items.Coins;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Factories.Controllers.Items.Coins
{
    public class CoinAnimationPresenterFactory
    {
        private readonly IPauseService _pauseService;

        public CoinAnimationPresenterFactory(IPauseService pauseService)
        {
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }

        public CoinPresenter Create(ICoinAnimationView coinAnimationView, Coin coin)
        {
            return new CoinPresenter(coinAnimationView, coin, _pauseService);
        }
    }
}