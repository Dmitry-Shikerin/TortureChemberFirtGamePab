using System;
using Sources.Controllers.Items.Coins;
using Sources.Domain.Constants;
using Sources.Domain.Items.Coins;
using Sources.Infrastructure.Factories.Controllers.Items.Coins;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Factories.Views;
using Sources.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Sources.Presentation.Views.Items.Coins;
using Sources.Presentation.Views.ObjectPolls;
using Sources.PresentationInterfaces.Views.Items.Coins;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Factories.Views.Items.Coins
{
    public class CoinViewFactory : IViewFactory<ICoinView, CoinView, Coin>
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly ObjectPool<CoinView> _objectPool;
        private readonly CoinPresenterFactory _coinPresenterFactory;

        public CoinViewFactory
        (
            CoinPresenterFactory coinPresenterFactory,
            IPrefabFactory prefabFactory,
            ObjectPool<CoinView> objectPool
        )
        {
            _coinPresenterFactory = coinPresenterFactory ??
                                    throw new ArgumentNullException(nameof(coinPresenterFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }

        public ICoinView Create(Coin coin, CoinView coinView)
        {
            CoinPresenter coinPresenter =
                _coinPresenterFactory.Create(coinView, coin);

            coinView.Construct(coinPresenter);

            return coinView;
        }

        public ICoinView Create(Coin coin)
        {
            CoinView coinView = CreateView();

            return Create(coin, coinView);
        }

        private CoinView CreateView() =>
            _prefabFactory.Create<CoinView>(Constant.PrefabPaths.CoinView)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<CoinView>();
    }
}