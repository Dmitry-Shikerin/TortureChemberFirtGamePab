using UnityEngine;

namespace Sources.Domain.Players.PlayerCameras
{
    public class PlayerCamera
    {
        private readonly Transform _cameraTransform;

        private const float AngularSpeed = 1f;
        
        public float AngleY { get; private set; }

        public void SetStartAngleY(float angleY) => 
            AngleY = angleY;

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