using UnityEngine;

namespace PresentationInterfaces.Views.Players
{
    public interface IPlayerCameraView
    {
        void Follow();
        void Rotate(float playerCameraAngleY);
        void SetTargetTransform(Transform targetTransform);
    }
}