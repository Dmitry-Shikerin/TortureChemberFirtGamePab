using Sources.DomainInterfaces.Items;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;

namespace Sources.Domain.Visitors
{
    public class Visitor
    {
        public Vector3 TargetPosition { get; private set; }
        public SeatPoint SeatPoint { get; private set; }
        public bool IsIdle { get; private set; }
        public bool CanSeat { get; private set; }
        public bool IsUnhappy { get; private set; }

        public void SetUnHappy(bool isUnhappy) => 
            IsUnhappy = isUnhappy;

        public void SetIdle(bool isIdle) => 
            IsIdle = isIdle;

        public void SetSeatPoint(SeatPoint seatPoint) => 
            SeatPoint = seatPoint;

        public void SetTargetPosition(Vector3 targetPosition) => 
            TargetPosition = targetPosition;

        public void SetCanSeat(bool canSeat) => 
            CanSeat = canSeat;
    }
}
