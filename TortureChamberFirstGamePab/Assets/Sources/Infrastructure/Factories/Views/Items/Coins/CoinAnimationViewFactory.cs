using System;
using JetBrains.Annotations;
using Sources.Controllers.Items.Coins;
using Sources.Domain.Items.Coins;
using Sources.Infrastructure.Factories.Controllers.Items.Coins;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Views.Items.Coins;
using Sources.Presentation.Views.ObjectPolls;
using Sources.PresentationInterfaces.Views.Items.Coins;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Factories.Views.Items.Coins
{
    public class CoinAnimationViewFactory
    {
        private const string CoinPrefabPath = "Views/Coin";
        private readonly IPrefabFactory _prefabFactory;
        private readonly ObjectPool<CoinAnimationView> _objectPool;
        private readonly CoinAnimationPresenterFactory _coinAnimationPresenterFactory;

        public CoinAnimationViewFactory(CoinAnimationPresenterFactory coinAnimationPresenterFactory,
            IPrefabFactory prefabFactory, ObjectPool<CoinAnimationView> objectPool)
        {
            _coinAnimationPresenterFactory = coinAnimationPresenterFactory ?? 
                                             throw new ArgumentNullException(nameof(coinAnimationPresenterFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }

        public ICoinAnimationView Create(CoinAnimation coinAnimation, CoinAnimationView coinAnimationView)
        {
            CoinAnimationPresenter coinAnimationPresenter =
                _coinAnimationPresenterFactory.Create(coinAnimationView, coinAnimation);
            
            coinAnimationView.Construct(coinAnimationPresenter);

            return coinAnimationView;
        }

        public ICoinAnimationView Create(CoinAnimation coinAnimation)
        {
            CoinAnimationView coinAnimationView = CreateView();

            return Create(coinAnimation, coinAnimationView);
        }

        private CoinAnimationView CreateView() =>
            _prefabFactory.Create<CoinAnimationView>(CoinPrefabPath)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<CoinAnimationView>();
    }
}