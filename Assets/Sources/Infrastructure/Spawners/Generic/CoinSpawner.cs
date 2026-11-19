using Domain.Items.Coins;
using Infrastructure.Services.ObjectPools;
using InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Presentation.Views.Items.Coins;
using PresentationInterfaces.Views.Items.Coins;

namespace Infrastructure.Spawners.Generic
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