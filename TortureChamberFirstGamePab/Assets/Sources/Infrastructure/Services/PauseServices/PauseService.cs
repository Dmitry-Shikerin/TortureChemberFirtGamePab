using System;
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

        public void Pause()
        {
            IsPaused = true;
            Time.timeScale = Constant.TimeScaleValue.Min;
        }

        public void PauseSound()
        {
            AudioListener.pause = true;
        }

        public void Continue()
        {
            IsPaused = false;
            Time.timeScale = Constant.TimeScaleValue.Max;
        }

        public void ContinueSound()
        {
            AudioListener.pause = false;
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