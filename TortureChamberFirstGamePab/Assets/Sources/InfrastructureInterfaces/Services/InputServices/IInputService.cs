using System;
using Sources.InfrastructureInterfaces.Factorys.Services;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public interface IInputService : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        public event Action<Vector2> MovementAxisChanged;
        public event Action<float> RunAxisChanged;
        public event Action<bool, bool> RotationChanged;
    }
}