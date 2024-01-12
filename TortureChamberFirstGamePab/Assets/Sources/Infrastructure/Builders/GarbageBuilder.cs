using System;
using JetBrains.Annotations;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Items.Garbeges;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Items.Garbages;
using Sources.Presentation.Views.ObjectPolls;
using Sources.PresentationInterfaces.Views.Garbages;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.Infrastructure.BuilderFactories
{
    public class GarbageBuilder
    {
        private const string GarbagePrefabPath = "Prefabs/Garbage";
        private readonly PrefabFactory _prefabFactory;
        private readonly GarbageViewFactory _garbageViewFactory;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly ObjectPool<GarbageView> _objectPool;

        public GarbageBuilder(PrefabFactory prefabFactory, GarbageViewFactory garbageViewFactory,
            ImageUIFactory imageUIFactory)
        {
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _garbageViewFactory = garbageViewFactory ?? throw new ArgumentNullException(nameof(garbageViewFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));

            _objectPool = new ObjectPool<GarbageView>();
        }
        
        //TODO это не билдер
        public IGarbageView Create()
        {
            GarbageView garbageView = _objectPool.Get<GarbageView>() ??
                                      _prefabFactory.Create<GarbageView>(GarbagePrefabPath)
                                          .AddComponent<PoolableObject>()
                                          .SetPool(_objectPool)
                                          .GetComponent<GarbageView>();

            _garbageViewFactory.Create(garbageView, _imageUIFactory);
            garbageView.Show();

            return garbageView;
        }
    }
}