using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.PauseServices
{
    public class PauseService : IPauseService
    {
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            IsPaused = true;
            Debug.Log("Pause");
        }

        public void Continue()
        {
            IsPaused = false;
            Debug.Log("Continue");
        }
        
        public async UniTask Yield(CancellationToken cancellationToken)
        {
            do
            {
                await UniTask.Yield(cancellationToken);
            } while (IsPaused);
        }
    }
}