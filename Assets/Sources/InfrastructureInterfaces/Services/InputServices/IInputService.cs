using System;
using Domain.Players.Inputs;
using InfrastructureInterfaces.Services.UpdateServices;

namespace InfrastructureInterfaces.Services.InputServices
{
    public interface IInputService : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        public event Action<bool, bool> RotationChanged;
        public event Action PauseButtonChanged;

        PlayerInput PlayerInput { get; }
    }
}