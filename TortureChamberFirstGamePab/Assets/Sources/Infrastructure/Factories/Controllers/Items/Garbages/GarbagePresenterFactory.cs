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
            return new GarbagePresenter
            (
                pickUpPointUIImages,
                garbageView,
                garbage
            );
        }
    }
}