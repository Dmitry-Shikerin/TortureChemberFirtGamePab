using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services.Cameras
{
    public interface ICameraDirectionService
    {
        Vector3 GetCameraDirection(Vector2 moveInput);
    }
}