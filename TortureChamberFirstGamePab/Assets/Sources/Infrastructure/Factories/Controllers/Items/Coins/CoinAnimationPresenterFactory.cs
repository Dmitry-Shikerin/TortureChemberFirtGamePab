using Sources.Controllers.Items.Coins;
using Sources.Domain.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Factories.Controllers.Items.Coins
{
    public class CoinAnimationPresenterFactory
    {
        public CoinAnimationPresenter Create(ICoinAnimationView coinAnimationView, CoinAnimation coinAnimation)
        {
            return new CoinAnimationPresenter(coinAnimationView, coinAnimation);
        }
    }
}