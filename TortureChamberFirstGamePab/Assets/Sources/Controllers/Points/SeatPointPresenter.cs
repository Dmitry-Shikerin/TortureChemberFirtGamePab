using System;
using Sources.Domain.Points;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Controllers.Points
{
    public class SeatPointPresenter : PresenterBase
    {
        private readonly ISeatPointView _seatPointView;
        private readonly SeatPoint _seatPoint;

        public SeatPointPresenter(ISeatPointView seatPointView, SeatPoint seatPoint)
        {
            _seatPointView = seatPointView ?? throw new ArgumentNullException(nameof(seatPointView));
            _seatPoint = seatPoint ?? throw new ArgumentNullException(nameof(seatPoint));
        }

        public bool IsOccupied => _seatPoint.IsOccupied;

        public void Occupy() => 
            _seatPoint.Occupy();

        public void UnOccupy() => 
            _seatPoint.UnOccupy();
    }
}