using System;
using Sources.InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace Sources.InfrastructureInterfaces.Services.VolumeServices
{
    public interface IVolumeService : IEnterable, IExitable
    {
        event Action VolumeChanged;
        
        float Volume { get; }
    }
}