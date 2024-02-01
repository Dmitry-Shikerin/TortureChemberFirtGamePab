using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sources.InfrastructureInterfaces.Services.PauseServices
{
    public interface IPauseService
    {
        bool IsPaused { get; }

        void Continue();
        void Pause();
        UniTask Yield(CancellationToken cancellationToken);
    }
}