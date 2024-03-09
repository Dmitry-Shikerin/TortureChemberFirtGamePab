using Sources.Domain.Items.Garbages;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Sources.Presentation.Views.Items.Garbages;
using Sources.PresentationInterfaces.Views.Items.Garbages;

namespace Sources.Infrastructure.Spawners.Generic
{
    public class GarbageSpawner : SpawnerBase<IGarbageView, GarbageView, Garbage>
    {
        public GarbageSpawner
        (
            IViewFactory<IGarbageView, GarbageView, Garbage> viewFactory,
            ObjectPool<GarbageView> objectPool
        ) : base(viewFactory, objectPool)
        {
        }
    }
}