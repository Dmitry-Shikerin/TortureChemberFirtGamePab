using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Services.PauseServices
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        bool IsSoundPaused { get; }
        event Action PauseActivated;
        event Action ContinueActivated;
        event Action PauseSoundActivated;
        event Action ContinueSoundActivated;

        void ContinueSound();
        void Continue();
        void PauseSound();
        void Pause();
        UniTask Yield(CancellationToken cancellationToken);
    }
}