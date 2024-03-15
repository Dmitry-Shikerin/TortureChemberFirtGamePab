using System;
using Scripts.InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace Scripts.InfrastructureInterfaces.Services.VolumeServices
{
    public interface IVolumeService : IEnterable, IExitable
    {
        event Action VolumeChanged;

        float Volume { get; }
    }
}