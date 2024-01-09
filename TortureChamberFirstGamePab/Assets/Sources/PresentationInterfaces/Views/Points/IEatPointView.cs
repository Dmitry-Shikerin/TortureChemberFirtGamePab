using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints.VisitorsPoints
{
    public interface IEatPointView
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public bool IsClear { get; }
        
        void Clean();
        void GetDirty();
    }
}