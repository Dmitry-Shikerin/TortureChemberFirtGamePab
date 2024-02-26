using Sources.Domain.Players.Inputs;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services.Movement
{
    public interface IMovementService
    {
        Quaternion GetDirectionRotation(Vector3 direction);
        float GetSpeedRotation();
        float GetMaxSpeed(PlayerInput playerInput, float currentAnimationSpeed, float runInput);
        Vector3 GetDirection(float runInput, float currentSpeed, Vector3 cameraDirection);
        float GetSpeed(float runInput, float currentSpeed, PlayerInput playerInput);
        bool IsIdle(PlayerInput playerInput);
    }
}