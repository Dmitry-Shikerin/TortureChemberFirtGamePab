using UnityEngine;

namespace Scripts.PresentationInterfaces.Views.Points
{
    public interface IVisitorPoint
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}