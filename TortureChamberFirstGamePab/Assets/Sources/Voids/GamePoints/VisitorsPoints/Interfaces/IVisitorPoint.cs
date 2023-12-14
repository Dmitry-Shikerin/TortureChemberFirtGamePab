using UnityEngine;

namespace Sources.Voids.GamePoints.VisitorsPoints.Interfaces
{
    public interface IVisitorPoint
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}