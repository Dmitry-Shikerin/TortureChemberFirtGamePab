using Sources.Domain.Items.Coins;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Views;
using Sources.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Sources.Presentation.Views.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Coins;

namespace Sources.Infrastructure.Spawners.Generic
{
    public class CoinSpawner : SpawnerBase<ICoinView, CoinView, Coin>
    {
        public CoinSpawner
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