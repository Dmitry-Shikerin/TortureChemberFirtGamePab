using Sources.Controllers.Points;
using Sources.Domain;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Infrastructure.Factories.Controllers.Points
{
    public class SeatPointPresenterFactory
    {
        public SeatPointPresenterFactory()
        {
            
        }

        public SeatPointPresenter Create(ISeatPointView seatPointView, SeatPoint seatPoint)
        {
            return new SeatPointPresenter
            (
                seatPointView,
                seatPoint
            );
        }
    }
}