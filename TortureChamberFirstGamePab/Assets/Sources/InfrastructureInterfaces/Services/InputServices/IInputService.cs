using System;
using Sources.Domain.Players.Inputs;
using Sources.InfrastructureInterfaces.Services.UpdateServices;

namespace Sources.InfrastructureInterfaces.Services.InputServices
{
    public interface IInputService : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        PlayerInput PlayerInput { get; }
        public event Action<bool, bool> RotationChanged;
        public event Action PauseButtonChanged;
    }
}