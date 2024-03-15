using System;
using Scripts.Domain.Players.Inputs;
using Scripts.InfrastructureInterfaces.Services.UpdateServices;

namespace Scripts.InfrastructureInterfaces.Services.InputServices
{
    public interface IInputService : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        public event Action<bool, bool> RotationChanged;
        public event Action PauseButtonChanged;

        PlayerInput PlayerInput { get; }
    }
}