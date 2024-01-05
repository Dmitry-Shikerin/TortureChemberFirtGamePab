using UnityEngine;

namespace Sources.Domain
{
    public class SeatPoint
    {
        public bool IsOccupied { get; private set; }

        public void SetIsOccupied(bool isOccupied)
        {
            IsOccupied = isOccupied;
            Debug.Log(IsOccupied);
        }
    }
}