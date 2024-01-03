using System;
using JetBrains.Annotations;
using Sources.Controllers.Items;
using Sources.Domain.Items.Garbages;
using Sources.Infrastructure.Factories.Controllers.Items.Garbages;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Presentation.UI;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.Presentation.Views.Items.Garbages;
using Sources.PresentationInterfaces.Views.Garbages;

namespace Sources.Infrastructure.Factories.Views.Items.Garbeges
{
    public class GarbageViewFactory
    {
        private readonly GarbagePresenterFactory _garbagePresenterFactory;

        public GarbageViewFactory(GarbagePresenterFactory garbagePresenterFactory)
        {
            _garbagePresenterFactory = garbagePresenterFactory ?? 
                                       throw new ArgumentNullException(nameof(garbagePresenterFactory));
        }

        public IGarbageView Create(GarbageView garbageView, ImageUIFactory imageUIFactory)
        {
            Garbage garbage = new Garbage();
            PickUpPointUI pickUpPointUI = garbageView.GetComponentInChildren<PickUpPointUI>();
            imageUIFactory.Create(pickUpPointUI.Image);
            imageUIFactory.Create(pickUpPointUI.BackgroundImage);
            
            
            GarbagePresenter garbagePresenter = _garbagePresenterFactory.Create(
                pickUpPointUI, garbageView, garbage);
            
            garbageView.Construct(garbagePresenter);

            return garbageView;
        }
    }
}