using Sources.Controllers.Items;
using Sources.Domain.Items.Garbages;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.PresentationInterfaces.Views.Garbages;

namespace Sources.Infrastructure.Factories.Controllers.Items.Garbages
{
    public class GarbagePresenterFactory
    {
        public GarbagePresenterFactory()
        {
        }

        public GarbagePresenter Create(PickUpPointUIImages pickUpPointUIImages, IGarbageView garbageView, Garbage garbage)
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