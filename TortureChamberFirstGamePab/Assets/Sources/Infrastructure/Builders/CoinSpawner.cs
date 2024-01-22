using System;
using Sources.Domain.Items.Coins;
using Sources.Infrastructure.Factories.Views.Items.Coins;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Builders
{
    public class CoinSpawner
    {
        private readonly CoinAnimationViewFactory _coinAnimationViewFactory;
        private readonly ObjectPool<CoinAnimationView> _objectPool;

        public CoinSpawner(CoinAnimationViewFactory coinAnimationViewFactory,
            ObjectPool<CoinAnimationView> objectPool)
        {
            _coinAnimationViewFactory = coinAnimationViewFactory ??
                                        throw new ArgumentNullException(nameof(coinAnimationViewFactory));

            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool)) ;
        }

        public ICoinAnimationView Spawn()
        {
            CoinAnimation coinAnimation = new CoinAnimation();

            return CreateFromPool(coinAnimation) ?? _coinAnimationViewFactory.Create(coinAnimation);
        }

        private ICoinAnimationView CreateFromPool(CoinAnimation coinAnimation)
        {
            CoinAnimationView coinAnimationView = _objectPool.Get<CoinAnimationView>();
        
            if (coinAnimationView == null)
                return null;
        
            return _coinAnimationViewFactory.Create(coinAnimation, coinAnimationView);
        }
    }
}