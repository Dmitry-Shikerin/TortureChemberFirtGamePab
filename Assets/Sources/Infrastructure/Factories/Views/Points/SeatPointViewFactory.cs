using System;
using Controllers.Points;
using Domain.Points;
using Infrastructure.Factories.Controllers.Points;
using Presentation.Views.GamePoints.VisitorsPoints;
using PresentationInterfaces.Views.Points;

namespace Infrastructure.Factories.Views.Points
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