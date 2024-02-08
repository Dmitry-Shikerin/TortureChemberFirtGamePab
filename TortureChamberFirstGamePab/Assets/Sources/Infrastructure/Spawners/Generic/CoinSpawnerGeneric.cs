using Sources.Domain.Items.Coins;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Views;
using Sources.Presentation.Views.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Spawners.Generic
{
    public class CoinSpawnerGeneric : SpawnerBase<ICoinView, CoinView, Coin>
    {
        public CoinSpawnerGeneric
        (
            IViewFactory<ICoinView, CoinView, Coin> viewFactory,
            ObjectPool<CoinView> objectPool
        ) : base(viewFactory, objectPool)
        {
        }

        protected override Coin CreateModel()
        {
            return new Coin();
        }
    }
}