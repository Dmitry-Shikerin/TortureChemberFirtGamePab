using UnityEngine;

namespace Sources.Presentation.Views.Cameras
{
    public class CinemachineCamera : View
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private bool _localMovement;
        [SerializeField] private bool _localRotation;

        private Vector3 Forward => _speed * Time.deltaTime * Vector3.forward;
        private Vector3 Upward => _speed * Time.deltaTime * Vector3.up;
        private Vector3 Downward => _speed * Time.deltaTime * Vector3.down;
        private Vector3 Backward => _speed * Time.deltaTime * Vector3.back;
        private Vector3 Leftward => _speed * Time.deltaTime * Vector3.left;
        private Vector3 Rightward => _speed * Time.deltaTime * Vector3.right;

        private void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }

        private void UpdatePosition()
        {
            if (Input.GetKey(KeyCode.Q)) transform.Translate(Upward, RelativeTo());

            if (Input.GetKey(KeyCode.E)) transform.Translate(Downward, RelativeTo());

            if (Input.GetKey(KeyCode.W)) transform.Translate(Forward, RelativeTo());

            if (Input.GetKey(KeyCode.S)) transform.Translate(Backward, RelativeTo());

            if (Input.GetKey(KeyCode.A)) transform.Translate(Leftward, RelativeTo());

            if (Input.GetKey(KeyCode.D)) transform.Translate(Rightward, RelativeTo());
        }

        private void UpdateRotation()
        {
            if (Input.GetKey(KeyCode.Z)) transform.Rotate(0, -0.1f * _rotateSpeed, 0);

            if (Input.GetKey(KeyCode.X)) transform.Rotate(0, 0.1f * _rotateSpeed, 0);

            if (Input.GetKey(KeyCode.R)) transform.Rotate(-0.1f * _rotateSpeed, 0, 0);

            if (Input.GetKey(KeyCode.F)) transform.Rotate(0.1f * _rotateSpeed, 0, 0);
        }

        private Space RelativeTo()
        {
            if (_localMovement)
                return Space.Self;

            return Space.World;
        }

        private void Translate(Vector3 direction)
        {
            transform.Translate(direction, Space.Self);
        }
    }
}