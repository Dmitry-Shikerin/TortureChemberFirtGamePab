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
        public bool IsSoundPaused { get; private set; }

        public int PauseListenersCount { get; private set; }
        public int SoundPauseListenersCount { get; private set; }

        public void Pause()
        {
            PauseListenersCount++;

            if (PauseListenersCount < 0)
                throw new InvalidOperationException(nameof(PauseListenersCount));
            
            IsPaused = true;
            PauseActivated?.Invoke();
            Time.timeScale = Constant.TimeScaleValue.Min;

            Debug.Log($"{nameof(PauseListenersCount)} {PauseListenersCount}");
        }

        public void PauseSound()
        {
            SoundPauseListenersCount++;

            if (SoundPauseListenersCount < 0)
                throw new InvalidOperationException(nameof(SoundPauseListenersCount));

            IsSoundPaused = true;
            PauseSoundActivated?.Invoke();
            Debug.Log($"{nameof(PauseListenersCount)} {PauseListenersCount}");
        }

        public void Continue()
        {
            PauseListenersCount--;

            Debug.Log($"{nameof(PauseListenersCount)} {PauseListenersCount}");
            if (PauseListenersCount > 0)
                return;

            if (PauseListenersCount < 0)
                throw new InvalidOperationException(nameof(PauseListenersCount));

            IsPaused = false;
            ContinueActivated?.Invoke();
            Time.timeScale = Constant.TimeScaleValue.Max;
        }

        public void ContinueSound()
        {
            SoundPauseListenersCount--;

            Debug.Log($"{nameof(SoundPauseListenersCount)} {SoundPauseListenersCount}");
            if (SoundPauseListenersCount > 0)
                return;

            if (SoundPauseListenersCount < 0)
                throw new InvalidOperationException(nameof(PauseListenersCount));

            IsSoundPaused = false;
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