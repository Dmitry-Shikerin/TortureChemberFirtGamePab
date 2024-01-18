using System;
using Sources.Domain.Players.Inputs;
using Sources.InfrastructureInterfaces.Services.UpdateServices;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services.InputServices
{
    public interface IInputService : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        public event Action<Vector2> MovementAxisChanged;
        public event Action<float> RunAxisChanged;
        public event Action<bool, bool> RotationChanged;
        
        PlayerInput PlayerInput { get; }
    }
}