using UnityEngine;

namespace Sources.Domain.Points
{
    public class SeatPoint
    {
        public bool IsOccupied { get; private set; }
        
        public void Occupy()
        {
            Debug.Log("место занято");
            IsOccupied = true;
        }

        public void UnOccupy()
        {
            Debug.Log("место свободно");
            IsOccupied = false;
        }
    }
}