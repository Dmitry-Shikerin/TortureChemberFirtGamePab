using System;
using Sources.InfrastructureInterfaces.Factorys.Services;

namespace Sources.InfrastructureInterfaces.Factories.Services
{
    public interface IUpdateService : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        event Action<float> ChangedUpdate;
        event Action<float> ChangedFixedUpdate;
        event Action<float> ChangedLateUpdate;
    }
}