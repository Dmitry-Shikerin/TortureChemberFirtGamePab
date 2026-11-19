using System;
using Controllers.Items;
using Domain.Items.Garbages;
using Presentation.Containers.UI.Images;
using PresentationInterfaces.Views.Items.Garbages;

namespace Infrastructure.Factories.Controllers.Items.Garbages
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