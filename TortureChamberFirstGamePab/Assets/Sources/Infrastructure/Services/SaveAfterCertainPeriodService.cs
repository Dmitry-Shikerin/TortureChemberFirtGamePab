using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Infrastructure.Services.LoadServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.LoadServices;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class SaveAfterCertainPeriodService : ISaveAfterCertainPeriodService
    {
        private CancellationTokenSource _cancellationTokenSource;
        private TimeSpan _timeSpan;
        
        public async void Enter(object payload = null)
        {
            if (payload == null)
                throw new NullReferenceException(nameof(payload));
            
            if (payload is not ILoadService loadService)
                throw new InvalidOperationException(nameof(payload));

            _cancellationTokenSource = new CancellationTokenSource();
            _timeSpan = TimeSpan.FromMinutes(Constant.SaveService.SaveDelay);
            
            await SaveAsync(loadService.Save, _cancellationTokenSource.Token);
        }
        
        public void Exit()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTask SaveAsync(Action saveAction, CancellationToken cancellationToken)
        {
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    await UniTask.Delay(_timeSpan, cancellationToken: cancellationToken);

                    saveAction.Invoke();
                    Debug.Log($"{nameof(SaveAfterCertainPeriodService)} SAVE");
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}