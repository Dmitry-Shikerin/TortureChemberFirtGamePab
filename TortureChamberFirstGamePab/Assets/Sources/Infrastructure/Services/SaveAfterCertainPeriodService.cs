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
        
        public async void Enter(object payload = null)
        {
            if (payload == null)
                throw new NullReferenceException(nameof(payload));
            
            if (payload is not ILoadService loadService)
                throw new InvalidOperationException(nameof(payload));

            _cancellationTokenSource = new CancellationTokenSource();
            
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
                //TODO сделать по аналогии
                while (cancellationToken.IsCancellationRequested == false)
                {
                    //TODo закешировать тайм спан
                    await UniTask.Delay(TimeSpan.FromMinutes(Constant.SaveService.SaveDelay),
                        cancellationToken: cancellationToken);

                    saveAction.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}