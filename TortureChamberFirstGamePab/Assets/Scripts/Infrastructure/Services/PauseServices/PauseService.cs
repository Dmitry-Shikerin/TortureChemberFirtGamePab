using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Domain.Constants;
using Scripts.InfrastructureInterfaces.Services.PauseServices;
using UnityEngine;

namespace Scripts.Infrastructure.Services.PauseServices
{
    public class PauseService : IPauseService
    {
        public event Action PauseActivated;
        public event Action ContinueActivated;
        public event Action PauseSoundActivated;
        public event Action ContinueSoundActivated;

        public int PauseListenersCount { get; private set; }
        public int SoundPauseListenersCount { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsSoundPaused { get; private set; }

        public void Pause()
        {
            PauseListenersCount++;

            if (PauseListenersCount < 0)
                throw new InvalidOperationException(nameof(PauseListenersCount));

            IsPaused = true;
            PauseActivated?.Invoke();
            Time.timeScale = TimeScaleValue.Min;
        }

        public void PauseSound()
        {
            SoundPauseListenersCount++;

            if (SoundPauseListenersCount < 0)
                throw new InvalidOperationException(nameof(SoundPauseListenersCount));

            IsSoundPaused = true;
            PauseSoundActivated?.Invoke();
        }

        public void Continue()
        {
            PauseListenersCount--;

            if (PauseListenersCount > 0)
                return;

            if (PauseListenersCount < 0)
                throw new InvalidOperationException(nameof(PauseListenersCount));

            IsPaused = false;
            ContinueActivated?.Invoke();
            Time.timeScale = TimeScaleValue.Max;
        }

        public void ContinueSound()
        {
            SoundPauseListenersCount--;

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
            }
            while (IsPaused);
        }
    }
}