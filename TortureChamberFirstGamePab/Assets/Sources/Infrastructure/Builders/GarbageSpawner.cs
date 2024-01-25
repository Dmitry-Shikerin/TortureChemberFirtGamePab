using System;
using Sources.Domain.Items.Garbages;
using Sources.Infrastructure.Factories.Views.Items.Garbeges;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Items.Garbages;
using Sources.PresentationInterfaces.Views.Garbages;

namespace Sources.Infrastructure.Builders
{
    public class GarbageSpawner
    {
        private readonly GarbageViewFactory _garbageViewFactory;
        private readonly ObjectPool<GarbageView> _objectPool;

        public GarbageSpawner(GarbageViewFactory garbageViewFactory, ObjectPool<GarbageView> objectPool)
        {
            _garbageViewFactory = garbageViewFactory ?? throw new ArgumentNullException(nameof(garbageViewFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }
        
        public IGarbageView Spawn()
        {
            Garbage garbage = new Garbage();
            
            return CreateFromPool(garbage) ?? _garbageViewFactory.Create(garbage);
        }

        private IGarbageView CreateFromPool(Garbage garbage)
        {
            GarbageView garbageView = _objectPool.Get<GarbageView>();

            if (garbageView == null)
                return null;

            return _garbageViewFactory.Create(garbage, garbageView);
        }
    }
}