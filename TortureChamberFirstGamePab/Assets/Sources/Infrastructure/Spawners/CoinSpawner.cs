using System;
using Sources.Domain.Items.Coins;
using Sources.Infrastructure.Factories.Views.Items.Coins;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Spawners;
using Sources.Presentation.Views.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Spawners
{
    //TODO можно обобщить эти спавнеры
    public class CoinSpawner : ISpawner<ICoinView>
    {
        private readonly CoinViewFactory _coinViewFactory;
        private readonly ObjectPool<CoinView> _objectPool;

        public CoinSpawner
        (
            CoinViewFactory coinViewFactory,
            ObjectPool<CoinView> objectPool
        )
        {
            _coinViewFactory = coinViewFactory ??
                                        throw new ArgumentNullException(nameof(coinViewFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }

        public ICoinView Spawn()
        {
            Coin coin = new Coin();

            return CreateFromPool(coin) ?? _coinViewFactory.Create(coin);
        }

        private ICoinView CreateFromPool(Coin coin)
        {
            CoinView coinView = _objectPool.Get<CoinView>();

            if (coinView == null)
                return null;

            return _coinViewFactory.Create(coin, coinView);
        }
    }
}