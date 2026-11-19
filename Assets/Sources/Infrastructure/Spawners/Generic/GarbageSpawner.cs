using Domain.Items.Garbages;
using Infrastructure.Services.ObjectPools;
using InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Presentation.Views.Items.Garbages;
using PresentationInterfaces.Views.Items.Garbages;

namespace Infrastructure.Spawners.Generic
{
    public class GarbageSpawner : SpawnerBase<IGarbageView, GarbageView, Garbage>
    {
        public GarbageSpawner(
            IViewFactory<IGarbageView, GarbageView, Garbage> viewFactory,
            ObjectPool<GarbageView> objectPool)
            : base(viewFactory, objectPool)
        {
        }
    }
}