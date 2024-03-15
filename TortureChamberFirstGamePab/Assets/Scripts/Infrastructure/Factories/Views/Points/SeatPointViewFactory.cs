using System;
using Scripts.Controllers.Points;
using Scripts.Domain.Points;
using Scripts.Infrastructure.Factories.Controllers.Points;
using Scripts.Presentation.Views.GamePoints.VisitorsPoints;
using Scripts.PresentationInterfaces.Views.Points;

namespace Scripts.Infrastructure.Factories.Views.Points
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
            SeatPointPresenter seatPointPresenter = _seatPointPresenterFactory.Create(seatPoint, seatPointView);

            seatPointView.Construct(seatPointPresenter);

            return seatPointView;
        }
    }
}