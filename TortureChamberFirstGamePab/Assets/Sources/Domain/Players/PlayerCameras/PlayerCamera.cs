using UnityEngine;

namespace Sources.Domain.Players.PlayerCameras
{
    public class PlayerCamera
    {
        private const float AngularSpeed = 1f;

        private readonly Transform _cameraTransform;

        public float AngleY { get; private set; }

        public void SetLeftRotation()
        {
            AngleY += AngularSpeed;
        }

        public void SetRightRotation()
        {
            AngleY -= AngularSpeed;
        }
    }
}