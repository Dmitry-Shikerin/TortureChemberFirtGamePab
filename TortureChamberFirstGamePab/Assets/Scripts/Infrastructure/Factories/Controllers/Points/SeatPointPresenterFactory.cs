using System;
using Scripts.Controllers.Points;
using Scripts.Domain.Points;
using Scripts.PresentationInterfaces.Views.Points;

namespace Scripts.Infrastructure.Factories.Controllers.Points
{
    public class SeatPointPresenterFactory
    {
        public SeatPointPresenter Create(SeatPoint seatPoint, ISeatPointView seatPointView)
        {
            if (seatPointView == null)
                throw new ArgumentNullException(nameof(seatPointView));

            if (seatPoint == null)
                throw new ArgumentNullException(nameof(seatPoint));

            return new SeatPointPresenter(seatPoint);
        }
    }
}