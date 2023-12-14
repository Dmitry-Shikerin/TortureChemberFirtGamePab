using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;

namespace Sources.Domain.Visitors
{
    public class Visitor
    {
        public Vector3 TargetPosition { get; private set; }
        public IVisitorPoint SeatPoint { get; private set; }
        public bool IsIdle { get; private set; }
        public bool CanSeat { get; private set; }

        public void SetIdle(bool isIdle)
        {
            IsIdle = isIdle;
        }

        public void SetSeatPoint(IVisitorPoint seatPoint)
        {
            SeatPoint = seatPoint;
        }
        
        public void SetTargetPosition(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
        }
    }
}
