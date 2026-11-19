using System;
using Controllers.Items.Coins;
using Domain.Constants;
using Domain.Items.Coins;
using Infrastructure.Factories.Controllers.Items.Coins;
using Infrastructure.Services.ObjectPools;
using InfrastructureInterfaces.Factories.Prefabs;
using InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Presentation.Views.Items.Coins;
using Presentation.Views.ObjectPolls;
using PresentationInterfaces.Views.Items.Coins;
using Unity.VisualScripting;

namespace Infrastructure.Factories.Views.Items.Coins
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