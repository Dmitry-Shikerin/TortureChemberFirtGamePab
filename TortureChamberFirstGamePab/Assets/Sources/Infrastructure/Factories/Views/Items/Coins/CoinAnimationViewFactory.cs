using System;
using JetBrains.Annotations;
using Sources.Controllers.Items.Coins;
using Sources.Domain.Items.Coins;
using Sources.Infrastructure.Factories.Controllers.Items.Coins;
using Sources.Presentation.Views.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Factories.Views.Items.Coins
{
    public class CoinAnimationViewFactory
    {
        private readonly CoinAnimationPresenterFactory _coinAnimationPresenterFactory;

        public CoinAnimationViewFactory(CoinAnimationPresenterFactory coinAnimationPresenterFactory)
        {
            _coinAnimationPresenterFactory = coinAnimationPresenterFactory ?? 
                                             throw new ArgumentNullException(nameof(coinAnimationPresenterFactory));
        }

        public ICoinAnimationView Create(CoinAnimationView coinAnimationView)
        {
            CoinAnimation coinAnimation = new CoinAnimation();
            CoinAnimationPresenter coinAnimationPresenter =
                _coinAnimationPresenterFactory.Create(coinAnimationView, coinAnimation);
            
            coinAnimationView.Construct(coinAnimationPresenter);

            return coinAnimationView;
        }
    }
}