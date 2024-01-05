using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints.VisitorsPoints
{
    public interface IEatPointView
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
    
        //TODO это должно быть в модели
        public bool IsClear { get; }

        public void SetIsClean(bool isClean);

    }
}