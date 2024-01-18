using UnityEngine;

namespace MyProject.Sources.PresentationInterfaces.Views
{
    public interface IPlayerCameraView
    {
        void Follow();
        void Rotate(float playerCameraAngleY);
        void SetTargetTransform(IPlayerMovementView playerMovementView);
        public void SetTargetTransform(Transform targetTransform);

    }
}