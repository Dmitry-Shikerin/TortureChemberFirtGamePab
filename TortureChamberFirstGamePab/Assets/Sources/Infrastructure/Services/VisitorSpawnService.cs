using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.Domain.GamePlays;
using Sources.Infrastructure.BuilderFactories;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Services
{
    public class VisitorSpawnService
    {
        private readonly UpdateService _updateService;
        private readonly GamePlay _gamePlay;
        private readonly VisitorBuilder _visitorBuilder;
        private readonly IObjectPool _objectPool;

        //TOdo наверно это отдельный сервис
        private int _activeVisitorsCount = 0;
        
        public VisitorSpawnService(UpdateService updateService, GamePlay gamePlay, VisitorBuilder visitorBuilder)
        {
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _gamePlay = gamePlay ?? throw new ArgumentNullException(nameof(gamePlay));
            _visitorBuilder = visitorBuilder ?? throw new ArgumentNullException(nameof(visitorBuilder));

            _objectPool = new ObjectPool<VisitorView>();
            
            //TODO не хочется делать вьюшку для подписки
            _updateService.ChangedUpdate += Update;
        }

        //TODO правильно ли тут все с юнитасками?
        private async void Update(float deltaTime)
        {
            if (TrySpawn())
            {
                await Spawn();
            }
        }

        private bool TrySpawn()
        {
            return _activeVisitorsCount < _gamePlay.MaximumVisitorsCapacity;
        }

        private async UniTask Spawn()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            VisitorSpawn();
        }

        private void VisitorSpawn()
        {
            IVisitorView visitorView = _objectPool.Get<VisitorView>() ?? _visitorBuilder.Create(_objectPool);
        }
    }
}