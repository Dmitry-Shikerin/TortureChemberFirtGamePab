using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Services.PauseServices
{
    public interface IPauseService
    {
        bool IsPaused { get; }

        void ContinueSound();
        void Continue();
        void PauseSound();
        void Pause();
        UniTask Yield(CancellationToken cancellationToken);
    }
}