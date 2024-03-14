using Sources.Presentation.Views.GamePoints.VisitorsPoints;
using UnityEngine;

namespace Sources.Domain.Visitors
{
    public class Visitor
    {
        public Vector3 TargetPosition { get; private set; }
        public SeatPointView SeatPointView { get; private set; }
        public bool IsIdle { get; private set; }
        public bool CanSeat { get; private set; }
        public bool IsUnhappy { get; private set; }
        public bool CanEat { get; private set; }

        public void FinishEating()
        {
            CanEat = false;
        }

        public void Eat()
        {
            CanEat = true;
        }

        public void SetUnHappy()
        {
            IsUnhappy = true;
        }

        public void SetHappy()
        {
            IsUnhappy = false;
        }

        public void SetIdle()
        {
            IsIdle = true;
        }

        public void SetMove()
        {
            IsIdle = false;
        }

        public void SetSeatPoint(SeatPointView seatPoint)
        {
            SeatPointView = seatPoint;
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
        }

        public void SetSeat()
        {
            CanSeat = true;
        }

        public void SetUnSeat()
        {
            CanSeat = false;
        }
    }
}