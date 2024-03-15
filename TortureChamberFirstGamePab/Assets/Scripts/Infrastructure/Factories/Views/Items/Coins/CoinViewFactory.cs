using System;
using Scripts.Controllers.Items.Coins;
using Scripts.Domain.Constants;
using Scripts.Domain.Items.Coins;
using Scripts.Infrastructure.Factories.Controllers.Items.Coins;
using Scripts.Infrastructure.Services.ObjectPools;
using Scripts.InfrastructureInterfaces.Factories.Prefabs;
using Scripts.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Scripts.Presentation.Views.Items.Coins;
using Scripts.Presentation.Views.ObjectPolls;
using Scripts.PresentationInterfaces.Views.Items.Coins;
using Unity.VisualScripting;

namespace Scripts.Infrastructure.Factories.Views.Items.Coins
{
    public class CoinViewFactory : IViewFactory<ICoinView, CoinView, Coin>
    {
        private readonly CoinPresenterFactory _coinPresenterFactory;
        private readonly ObjectPool<CoinView> _objectPool;
        private readonly IPrefabFactory _prefabFactory;

        public CoinViewFactory(
            CoinPresenterFactory coinPresenterFactory,
            IPrefabFactory prefabFactory,
            ObjectPool<CoinView> objectPool)
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

        private CoinView CreateView()
        {
            return _prefabFactory.Create<CoinView>(PrefabPath.CoinView)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<CoinView>();
        }
    }
}