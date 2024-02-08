using System;
using Sources.Controllers.Items;
using Sources.Domain.Constants;
using Sources.Domain.Items.Garbages;
using Sources.Infrastructure.Factories.Controllers.Items.Garbages;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Factories.Views;
using Sources.Presentation.Views.Items.Garbages;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.PresentationInterfaces.Views.Items.Garbages;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Factories.Views.Items.Garbeges
{
    public class GarbageViewFactory : IViewFactory<IGarbageView, GarbageView, Garbage>
    {
        private readonly GarbagePresenterFactory _garbagePresenterFactory;
        private readonly ObjectPool<GarbageView> _objectPool;
        private readonly IPrefabFactory _prefabFactory;
        private readonly ImageUIFactory _imageUIFactory;

        public GarbageViewFactory
        (
            GarbagePresenterFactory garbagePresenterFactory,
            ObjectPool<GarbageView> objectPool,
            IPrefabFactory prefabFactory,
            ImageUIFactory imageUIFactory
        )
        {
            _garbagePresenterFactory = garbagePresenterFactory ??
                                       throw new ArgumentNullException(nameof(garbagePresenterFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
        }

        public IGarbageView Create(Garbage garbage, GarbageView garbageView)
        {
            PickUpPointUIImages pickUpPointUIImages = garbageView.GetComponentInChildren<PickUpPointUIImages>();

            _imageUIFactory.Create(pickUpPointUIImages.Image);
            _imageUIFactory.Create(pickUpPointUIImages.BackgroundImage);

            GarbagePresenter garbagePresenter = _garbagePresenterFactory.Create(
                pickUpPointUIImages, garbageView, garbage);

            garbageView.Construct(garbagePresenter);

            return garbageView;
        }

        public IGarbageView Create(Garbage garbage)
        {
            GarbageView garbageView = CreateView();

            return Create(garbage, garbageView);
        }

        private GarbageView CreateView() =>
            _prefabFactory.Create<GarbageView>(Constant.PrefabPaths.GarbageView)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<GarbageView>();
    }
}