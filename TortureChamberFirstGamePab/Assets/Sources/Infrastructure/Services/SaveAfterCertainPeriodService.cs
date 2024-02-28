using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Infrastructure.Services.LoadServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.LoadServices;

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
        
        //TODO указать чколько монеток добавится за рекламу
        //TODO добавить все стулья 
        //TODO настроить баланс игры
        //TODO настроить цены улучшений
        //TODO проверить работу всех сервисов
        //TODO спросить про шрифт в лидерборде
        //TODO у пива изза того что роядом еще есть кколлайдеры плохо работает поинт
        //TODO поставить столы пошире
        //TODO уменьшить шанс на мусор
        //TODO увеличить время ожидания
        //TODO перезапеч навмеш
        //TODO поработать со светом
        //TODO ускорить уборку мусора
        //TODO сменить точку спавна игрока
        //TODO посмотреть на добавляемое количество счастья
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
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}