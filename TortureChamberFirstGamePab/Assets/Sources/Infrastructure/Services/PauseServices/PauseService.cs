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

        //TODO сеттинг не соответствует громкости
        //Todo не выключаются звукифуц
        //TODO не работает регулировка громкости
        //TODO если я в паузе то при смене фокуса на игру она убирается даже если я в меню паузы
        //TODO сделать диммер для окошек паузы
        public void PauseSound()
        {
            PauseSoundActivated?.Invoke();
        }

        //TODO разобратся с паузой
        //TODO увеличить размер триггеров
        public void Continue()
        {
            IsPaused = false;
            ContinueActivated?.Invoke();
            Time.timeScale = Constant.TimeScaleValue.Max;
        }

        public void ContinueSound()
        {
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