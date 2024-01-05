using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Domain;
using Sources.PresentationInterfaces.Views.Points;

namespace Sources.Infrastructure.Factories.Controllers.Points
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

        public void SetIsOccupied(bool isOccupied)
        {
            _seatPoint.SetIsOccupied(isOccupied);
        }
    }
}