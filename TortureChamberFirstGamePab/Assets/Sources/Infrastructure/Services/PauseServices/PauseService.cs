using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using UnityEngine;

namespace Sources.Infrastructure.Services.PauseServices
{
    public class PauseService : IPauseService
    {
        public bool IsPaused { get; private set; }

        //TODO убрать магические числа
        public void Pause()
        {
            IsPaused = true;
            Time.timeScale = Constant.TimeScaleValue.Min;
            AudioListener.pause = true;
            Debug.Log("Pause");
        }

        public void Continue()
        {
            IsPaused = false;
            Time.timeScale = Constant.TimeScaleValue.Max;
            AudioListener.pause = false;
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