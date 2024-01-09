using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.Domain.GamePlays;
using Sources.Domain.Visitors;
using Sources.Infrastructure.BuilderFactories;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Services
{
    public class VisitorSpawnService
    {
        private readonly GamePlay _gamePlay;
        private readonly VisitorBuilder _visitorBuilder;
        private readonly VisitorCounter _visitorCounter;
        private readonly IObjectPool _objectPool;

        
        public VisitorSpawnService(GamePlay gamePlay, 
            VisitorBuilder visitorBuilder, VisitorCounter visitorCounter)
        {
            _gamePlay = gamePlay ?? throw new ArgumentNullException(nameof(gamePlay));
            _visitorBuilder = visitorBuilder ?? throw new ArgumentNullException(nameof(visitorBuilder));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));

            _objectPool = new ObjectPool<VisitorView>();
        }

        private bool CanSpawn => _visitorCounter.ActiveVisitorsCount < _gamePlay.MaximumVisitorsCapacity;
        
        //TODO не спавнит новых посетителей
        public async void SpawnVisitorAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (CanSpawn)
                {
                    Spawn();
                    //TODO как сюда засунуть токен?
                    await UniTask.Delay(TimeSpan.FromSeconds(5));
                }

                await UniTask.Yield(cancellationToken);
            }
        }

        private void Spawn()
        {
            IVisitorView visitorView = _objectPool.Get<VisitorView>() ?? _visitorBuilder.Create(_objectPool);
            _visitorCounter.AdвActiveVisitorsCount();
        }
    }
}