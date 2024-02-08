using System;
using Sources.Domain.Players.Inputs;
using Sources.InfrastructureInterfaces.Services.UpdateServices;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services.InputServices
{
    public interface IInputService : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        public event Action<bool, bool> RotationChanged;
        public event Action PauseButtonChanged;
        
        PlayerInput PlayerInput { get; }
    }
}