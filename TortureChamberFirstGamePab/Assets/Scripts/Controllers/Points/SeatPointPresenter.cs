using System;
using Scripts.Domain.Points;

namespace Scripts.Controllers.Points
{
    public class SeatPointPresenter : PresenterBase
    {
        private readonly SeatPoint _seatPoint;

        public SeatPointPresenter(SeatPoint seatPoint)
        {
            _seatPoint = seatPoint ?? throw new ArgumentNullException(nameof(seatPoint));
        }

        public bool IsOccupied => _seatPoint.IsOccupied;

        public void Occupy()
        {
            _seatPoint.Occupy();
        }

        public void UnOccupy()
        {
            _seatPoint.UnOccupy();
        }
    }
}