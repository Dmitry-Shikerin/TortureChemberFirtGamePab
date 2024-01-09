using UnityEngine;

namespace Sources.Domain
{
    public class SeatPoint
    {
        public bool IsOccupied { get; private set; }
        
        public void Occupy() => 
            IsOccupied = true;

        public void UnOccupy() => 
            IsOccupied = false;
    }
}