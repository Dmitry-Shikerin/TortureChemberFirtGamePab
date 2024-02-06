using Sources.Presentation.Voids.GamePoints.VisitorsPoints.Interfaces;
using UnityEngine;

namespace Sources.Presentation.Voids.GamePoints.VisitorsPoints
{
    public class OutDoorPoint : MonoBehaviour, IVisitorPoint
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}
