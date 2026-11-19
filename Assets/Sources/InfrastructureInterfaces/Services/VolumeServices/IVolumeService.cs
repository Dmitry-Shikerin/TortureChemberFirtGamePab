using System;
using InfrastructureInterfaces.StateMachines.SceneStateMachines;

namespace InfrastructureInterfaces.Services.VolumeServices
{
    public interface IVolumeService : IEnterable, IExitable
    {
        event Action VolumeChanged;

        float Volume { get; }
    }
}