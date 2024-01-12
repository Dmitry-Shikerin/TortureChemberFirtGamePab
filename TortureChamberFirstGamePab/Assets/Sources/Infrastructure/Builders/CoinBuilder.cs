using System;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Items.Coins;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Items.Coins;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views.Items.Coins;
using Unity.VisualScripting;

namespace Sources.Infrastructure.BuilderFactories
{
    public class CoinBuilder
    {
        private const string CoinPrefabPath = "Views/Coin";
        private readonly PrefabFactory _prefabFactory;
        private readonly CoinAnimationViewFactory _coinAnimationViewFactory;
        private readonly ObjectPool<CoinAnimationView> _objectPool;

        public CoinBuilder(PrefabFactory prefabFactory, CoinAnimationViewFactory coinAnimationViewFactory
        )
        {
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _coinAnimationViewFactory = coinAnimationViewFactory ??
                                        throw new ArgumentNullException(nameof(coinAnimationViewFactory));

            _objectPool = new ObjectPool<CoinAnimationView>();
        }

        //TODO это не билдер а спавн сервис
        public ICoinAnimationView Create()
        {
            // if (_objectPool.Count > 0)
            // {
            //     CoinAnimationView coinAnimationView = _objectPool.Get<CoinAnimationView>();
            //     coinAnimationView.Show();
            // }

            CoinAnimationView coinAnimationViewPrefab = _objectPool.Get<CoinAnimationView>() ??
                                                        _prefabFactory.Create<CoinAnimationView>(CoinPrefabPath)
                                                            .AddComponent<PoolableObject>()
                                                            .SetPool(_objectPool)
                                                            .GetComponent<CoinAnimationView>();

            // coinAnimationViewPrefab.AddComponent<PoolableObject>().SetPool(_objectPool);

            
            _coinAnimationViewFactory.Create(coinAnimationViewPrefab);
            coinAnimationViewPrefab.Show();

            return coinAnimationViewPrefab;
        }
    }
}