using System;
using Scripts.Domain.Constants;
using Scripts.Domain.Items.Garbages;
using Scripts.Infrastructure.Factories.Controllers.Items.Garbages;
using Scripts.Infrastructure.Factories.Views.UI;
using Scripts.Infrastructure.Factories.Views.UI.AudioSources;
using Scripts.Infrastructure.Services.ObjectPools;
using Scripts.InfrastructureInterfaces.Factories.Prefabs;
using Scripts.InfrastructureInterfaces.Factories.Views.ViewFactories.Generic.Triple;
using Scripts.Presentation.Containers.UI.Images;
using Scripts.Presentation.Views.Items.Garbages;
using Scripts.Presentation.Views.ObjectPolls;
using Scripts.PresentationInterfaces.Views.Items.Garbages;
using Unity.VisualScripting;

namespace Scripts.Infrastructure.Factories.Views.Items.Garbeges
{
    public class GarbageViewFactory : IViewFactory<IGarbageView, GarbageView, Garbage>
    {
        private readonly AudioSourceUIFactory _audioSourceUIFactory;
        private readonly GarbagePresenterFactory _garbagePresenterFactory;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly ObjectPool<GarbageView> _objectPool;
        private readonly IPrefabFactory _prefabFactory;

        public GarbageViewFactory(
            GarbagePresenterFactory garbagePresenterFactory,
            ObjectPool<GarbageView> objectPool,
            IPrefabFactory prefabFactory,
            ImageUIFactory imageUIFactory,
            AudioSourceUIFactory audioSourceUIFactory)
        {
            _garbagePresenterFactory = garbagePresenterFactory ??
                                       throw new ArgumentNullException(nameof(garbagePresenterFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _audioSourceUIFactory = audioSourceUIFactory ??
                                    throw new ArgumentNullException(nameof(audioSourceUIFactory));
        }

        public IGarbageView Create(Garbage garbage, GarbageView garbageView)
        {
            var pickUpPointUIImages = garbageView.GetComponentInChildren<PickUpPointUIImages>();

            _imageUIFactory.Create(pickUpPointUIImages.Image);
            _imageUIFactory.Create(pickUpPointUIImages.BackgroundImage);

            _audioSourceUIFactory.Create(garbage, garbageView.AudioSourceUI);

            var garbagePresenter = _garbagePresenterFactory.Create(
                pickUpPointUIImages,
                garbageView,
                garbage);

            garbageView.Construct(garbagePresenter);

            return garbageView;
        }

        public IGarbageView Create(Garbage garbage)
        {
            var garbageView = CreateView();

            return Create(garbage, garbageView);
        }

        private GarbageView CreateView()
        {
            return _prefabFactory.Create<GarbageView>(PrefabPath.GarbageView)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<GarbageView>();
        }
    }
}