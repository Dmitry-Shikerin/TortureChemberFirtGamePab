using Scripts.Domain.Items.Garbages;
using Scripts.Infrastructure.Services.ObjectPools;
using Scripts.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Scripts.Presentation.Views.Items.Garbages;
using Scripts.PresentationInterfaces.Views.Items.Garbages;

namespace Scripts.Infrastructure.Spawners.Generic
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