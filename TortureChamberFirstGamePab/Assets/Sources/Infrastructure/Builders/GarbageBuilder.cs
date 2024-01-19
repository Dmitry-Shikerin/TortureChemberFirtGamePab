using System;
using JetBrains.Annotations;
using Sources.Domain.Items.Garbages;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Items.Garbeges;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Items.Garbages;
using Sources.Presentation.Views.ObjectPolls;
using Sources.PresentationInterfaces.Views.Garbages;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Builders
{
    public class GarbageBuilder
    {
        private const string GarbagePrefabPath = "Prefabs/Garbage";
        private readonly PrefabFactory _prefabFactory;
        private readonly GarbageViewFactory _garbageViewFactory;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly ObjectPool<GarbageView> _objectPool;

        public GarbageBuilder(PrefabFactory prefabFactory, GarbageViewFactory garbageViewFactory,
            ImageUIFactory imageUIFactory, ObjectPool<GarbageView> objectPool)
        {
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _garbageViewFactory = garbageViewFactory ?? throw new ArgumentNullException(nameof(garbageViewFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }
        
        //TODO это не билдер
        public IGarbageView Build()
        {
            //TODO возможно переместить это во вью фектори

            // GarbageView garbageView = _objectPool.Get<GarbageView>() ??
            //                           _prefabFactory.Create<GarbageView>(GarbagePrefabPath)
            //                               .AddComponent<PoolableObject>()
            //                               .SetPool(_objectPool)
            //                               .GetComponent<GarbageView>();

            Garbage garbage = new Garbage();
            
            return CreateFromPool(garbage) ?? _garbageViewFactory.Create(garbage);
        }

        public IGarbageView CreateFromPool(Garbage garbage)
        {
            GarbageView garbageView = _objectPool.Get<GarbageView>();

            if (garbageView == null)
                return null;

            return garbageView;
        }
    }
}