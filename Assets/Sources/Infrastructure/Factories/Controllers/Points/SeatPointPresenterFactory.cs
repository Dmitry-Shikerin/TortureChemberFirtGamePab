using System;
using Controllers.Points;
using Domain.Points;
using PresentationInterfaces.Views.Points;

namespace Infrastructure.Factories.Controllers.Points
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