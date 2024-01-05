using System;
using Sources.Domain;
using Sources.Infrastructure.Factories.Controllers.Points;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Infrastructure.Factories.Views.Points
{
    public class SeatPointViewFactory
    {
        private readonly SeatPointPresenterFactory _seatPointPresenterFactory;

        public SeatPointViewFactory(SeatPointPresenterFactory seatPointPresenterFactory)
        {
            _seatPointPresenterFactory = seatPointPresenterFactory ?? 
                                         throw new ArgumentNullException(nameof(seatPointPresenterFactory));
        }

        public ISeatPointView Create(SeatPointView seatPointView)
        {
            SeatPoint seatPoint = new SeatPoint();
            SeatPointPresenter seatPointPresenter = _seatPointPresenterFactory.Create(seatPointView, seatPoint);
            
            seatPointView.Construct(seatPointPresenter);

            return seatPointView;
        }
    }
}