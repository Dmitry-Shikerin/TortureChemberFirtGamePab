using System;
using Sources.Domain.Points;
using Sources.Infrastructure.Factories.Controllers.Points;
using Sources.Presentation.Views.GamePoints.VisitorsPoints;
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
            var seatPoint = new SeatPoint();
            var seatPointPresenter = _seatPointPresenterFactory.Create(seatPoint, seatPointView);

            seatPointView.Construct(seatPointPresenter);

            return seatPointView;
        }
    }
}