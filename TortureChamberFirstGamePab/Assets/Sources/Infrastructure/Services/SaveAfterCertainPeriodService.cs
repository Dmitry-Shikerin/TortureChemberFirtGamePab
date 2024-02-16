using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Infrastructure.Services.LoadServices;
using Sources.InfrastructureInterfaces.Services;

namespace Sources.Infrastructure.Services
{
    public class SaveAfterCertainPeriodService : ISaveAfterCertainPeriodService
    {
        private CancellationTokenSource _cancellationTokenSource;

        private bool _isPlaying;
        
        public async void Enter(object payload = null)
        {
            if (payload == null)
                throw new NullReferenceException(nameof(payload));
            
            if (payload is not ILoadService loadService)
                throw new InvalidOperationException(nameof(payload));

            _cancellationTokenSource = new CancellationTokenSource();
            
            await SaveAsync(loadService.Save);
        }

        public void Exit()
        {
            _isPlaying = false;
            
            _cancellationTokenSource.Cancel();
        }

        private async UniTask SaveAsync(Action saveAction)
        {
            _isPlaying = true;

            //TODO как избавится от этого трай кетча?
            //TODO будут ли поломки если его не будет?
            try
            {
                while (_isPlaying)
                {
                    await UniTask.Delay(TimeSpan.FromMinutes(Constant.SaveService.SaveDelay),
                        cancellationToken: _cancellationTokenSource.Token);

                    saveAction.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}