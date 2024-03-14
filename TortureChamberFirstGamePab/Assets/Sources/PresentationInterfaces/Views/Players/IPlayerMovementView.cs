using UnityEngine;

namespace MyProject.Sources.PresentationInterfaces.Views
{
    public interface IPlayerMovementView
    {
        Vector3 Position { get; }
        Transform Transform { get; }
        float RotationAngle { get; }

        public void Move(Vector3 direction);
        public void Rotate(Quaternion look, float speed);
        void SetPosition(Vector3 position);
        void SetAngle(float angle);
    }
}