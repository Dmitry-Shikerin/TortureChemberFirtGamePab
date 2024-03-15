using System;
using Scripts.Controllers.Items;
using Scripts.Domain.Items.Garbages;
using Scripts.Presentation.Containers.UI.Images;
using Scripts.PresentationInterfaces.Views.Items.Garbages;

namespace Scripts.Infrastructure.Factories.Controllers.Items.Garbages
{
    public class GarbagePresenterFactory
    {
        public GarbagePresenter Create(
            PickUpPointUIImages pickUpPointUIImages,
            IGarbageView garbageView,
            Garbage garbage)
        {
            if (pickUpPointUIImages == null)
                throw new ArgumentNullException(nameof(pickUpPointUIImages));

            if (garbageView == null)
                throw new ArgumentNullException(nameof(garbageView));

            if (garbage == null)
                throw new ArgumentNullException(nameof(garbage));

            return new GarbagePresenter(
                pickUpPointUIImages,
                garbageView,
                garbage);
        }
    }
}