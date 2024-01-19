using System;
using JetBrains.Annotations;
using Sources.Domain.Items.Coins;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Items.Coins;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Items.Coins;
using Sources.Presentation.Views.ObjectPolls;
using Sources.PresentationInterfaces.Views.Items.Coins;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Builders
{
    public class CoinBuilder
    {
        private readonly CoinAnimationViewFactory _coinAnimationViewFactory;
        private readonly ObjectPool<CoinAnimationView> _objectPool;

        public CoinBuilder(CoinAnimationViewFactory coinAnimationViewFactory,
            ObjectPool<CoinAnimationView> objectPool)
        {
            _coinAnimationViewFactory = coinAnimationViewFactory ??
                                        throw new ArgumentNullException(nameof(coinAnimationViewFactory));

            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool)) ;
        }

        public ICoinAnimationView Build()
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