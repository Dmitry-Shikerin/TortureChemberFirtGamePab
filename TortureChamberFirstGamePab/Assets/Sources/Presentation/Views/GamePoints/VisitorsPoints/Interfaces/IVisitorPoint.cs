using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints.VisitorsPoints.Interfaces
{
    public interface IVisitorPoint
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}