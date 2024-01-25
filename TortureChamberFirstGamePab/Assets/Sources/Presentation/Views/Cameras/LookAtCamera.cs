using UnityEngine;

namespace Sources.Presentation.Views.Cameras
{
    public class LookAtCamera : View
    {
        private Camera _mainCamera;

        private void Start() =>
            _mainCamera = Camera.main;

        //TODO придумать ка убрать отсюда этот апдейт
        private void Update()
        {
            Quaternion rotation = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}