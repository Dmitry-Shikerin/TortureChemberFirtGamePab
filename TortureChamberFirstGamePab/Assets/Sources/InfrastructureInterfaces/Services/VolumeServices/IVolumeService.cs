using System;
using Sources.InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace Sources.InfrastructureInterfaces.Services.VolumeServices
{
    public interface IVolumeService : IEnterable, IExitable
    {
        float Volume { get; }
        event Action VolumeChanged;
    }
}