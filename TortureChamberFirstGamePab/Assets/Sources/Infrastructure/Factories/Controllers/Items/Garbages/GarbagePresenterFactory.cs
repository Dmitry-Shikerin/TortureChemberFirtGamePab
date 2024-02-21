using System;
using Sources.Controllers.Items;
using Sources.Domain.Items.Garbages;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.PresentationInterfaces.Views.Items.Garbages;

namespace Sources.Infrastructure.Factories.Controllers.Items.Garbages
{
    public class GarbagePresenterFactory
    {
        public GarbagePresenter Create
        (
            PickUpPointUIImages pickUpPointUIImages,
            IGarbageView garbageView,
            Garbage garbage
        )
        {
            if (pickUpPointUIImages == null) 
                throw new ArgumentNullException(nameof(pickUpPointUIImages));
            
            if (garbageView == null) 
                throw new ArgumentNullException(nameof(garbageView));
            
            if (garbage == null) 
                throw new ArgumentNullException(nameof(garbage));
            
            return new GarbagePresenter
            (
                pickUpPointUIImages,
                garbageView,
                garbage
            );
        }
    }
}