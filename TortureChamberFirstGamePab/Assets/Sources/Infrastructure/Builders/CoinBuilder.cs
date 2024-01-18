using System;
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
            //TODO возможно переместить это во вью фектори
            CoinAnimationView coinAnimationViewPrefab = _objectPool.Get<CoinAnimationView>() ??
                                                        _prefabFactory.Create<CoinAnimationView>(CoinPrefabPath)
                                                            .AddComponent<PoolableObject>()
                                                            .SetPool(_objectPool)
                                                            .GetComponent<CoinAnimationView>();

            _coinAnimationViewFactory.Create(coinAnimationViewPrefab);
            coinAnimationViewPrefab.Show();

            return coinAnimationViewPrefab;
        }
    }
}