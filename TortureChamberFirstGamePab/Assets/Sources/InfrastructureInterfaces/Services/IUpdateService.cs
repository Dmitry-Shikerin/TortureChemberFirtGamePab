using System;
using Sources.InfrastructureInterfaces.Services.UpdateServices;

namespace Sources.InfrastructureInterfaces.Services
{
    public interface IUpdateService : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        event Action<float> ChangedUpdate;
        event Action<float> ChangedFixedUpdate;
        event Action<float> ChangedLateUpdate;
    }
}