using Scripts.PresentationInterfaces.Views.Points;
using UnityEngine;

namespace Scripts.Presentation.Views.GamePoints.VisitorsPoints
{
    public class OutDoorPoint : MonoBehaviour, IVisitorPoint
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}