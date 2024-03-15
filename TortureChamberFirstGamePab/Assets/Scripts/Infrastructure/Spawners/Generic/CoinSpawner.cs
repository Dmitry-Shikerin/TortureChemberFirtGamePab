using Scripts.Domain.Items.Coins;
using Scripts.Infrastructure.Services.ObjectPools;
using Scripts.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Scripts.Presentation.Views.Items.Coins;
using Scripts.PresentationInterfaces.Views.Items.Coins;

namespace Scripts.Infrastructure.Spawners.Generic
{
    public class CoinSpawner : SpawnerBase<ICoinView, CoinView, Coin>
    {
        public CoinSpawner(
            IViewFactory<ICoinView, CoinView, Coin> viewFactory,
            ObjectPool<CoinView> objectPool)
            : base(viewFactory, objectPool)
        {
        }
    }
}