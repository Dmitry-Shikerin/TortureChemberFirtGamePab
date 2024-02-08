using Sources.Domain.Items.Garbages;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Views;
using Sources.Presentation.Views.Items.Garbages;
using Sources.PresentationInterfaces.Views.Items.Garbages;

namespace Sources.Infrastructure.Spawners.Generic
{
    public class GarbageSpawnerGeneric : SpawnerBase<IGarbageView, GarbageView, Garbage>
    {
        public GarbageSpawnerGeneric
        (
            IViewFactory<IGarbageView, GarbageView, Garbage> viewFactory,
            ObjectPool<GarbageView> objectPool
        ) : base(viewFactory, objectPool)
        {
        }

        protected override Garbage CreateModel()
        {
            return new Garbage();
        }
    }
}