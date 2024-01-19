using System;
using JetBrains.Annotations;
using Sources.Controllers.Items;
using Sources.Domain.Constants;
using Sources.Domain.Items.Garbages;
using Sources.Infrastructure.Factories.Controllers.Items.Garbages;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Items.Coins;
using Sources.Presentation.Views.Items.Garbages;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.PresentationInterfaces.Views.Garbages;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Factories.Views.Items.Garbeges
{
    public class GarbageViewFactory
    {
        private const string GarbagePrefabPath = "Prefabs/Garbage";
        private readonly GarbagePresenterFactory _garbagePresenterFactory;
        private readonly ObjectPool<GarbageView> _objectPool;
        private readonly PrefabFactory _prefabFactory;
        private readonly ImageUIFactory _imageUIFactory;

        public GarbageViewFactory(GarbagePresenterFactory garbagePresenterFactory,
            ObjectPool<GarbageView> objectPool, PrefabFactory prefabFactory,
            ImageUIFactory imageUIFactory)
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
            
            //TODO не знаю куда засунуть
            pickUpPointUIImages.BackgroundImage.SetFillAmount(Constant.MaximumAmountFillingImage);
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
            _prefabFactory.Create<GarbageView>(GarbagePrefabPath)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<GarbageView>();
    }
}