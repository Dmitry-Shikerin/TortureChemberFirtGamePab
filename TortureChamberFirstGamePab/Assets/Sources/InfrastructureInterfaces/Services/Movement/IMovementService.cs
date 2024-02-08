using Sources.Domain.Players.Inputs;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services.Movement
{
    public interface IMovementService
    {
        Quaternion GetDirectionRotation(Vector3 direction);
        float GetSpeedRotation();
        float GetMaxSpeed(PlayerInput playerInput, float runInput);
        Vector3 GetDirection(float runInput, Vector3 cameraDirection);
    }
}