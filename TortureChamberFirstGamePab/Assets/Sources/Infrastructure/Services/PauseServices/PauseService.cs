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
        public event Action PauseActivated;
        public event Action ContinueActivated;
        public event Action PauseSoundActivated;
        public event Action ContinueSoundActivated;

        public bool IsPaused { get; private set; }

        public void Pause()
        {
            IsPaused = true;
            PauseActivated?.Invoke();
            Time.timeScale = Constant.TimeScaleValue.Min;
        }

        public void PauseSound()
        {
            // AudioListener.pause = true;
            PauseSoundActivated?.Invoke();
        }

        public void Continue()
        {
            IsPaused = false;
            ContinueActivated?.Invoke();
            Time.timeScale = Constant.TimeScaleValue.Max;
        }

        public void ContinueSound()
        {
            // AudioListener.pause = false;
            ContinueSoundActivated?.Invoke();
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