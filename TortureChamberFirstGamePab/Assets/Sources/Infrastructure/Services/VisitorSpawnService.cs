using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Services
{
    public class VisitorSpawnService
    {
        private readonly GamePlay _gamePlay;
        private readonly VisitorCounter _visitorCounter;
        private readonly IPrefabFactory _prefabFactory;
        private readonly ObjectPool<VisitorView> _objectPool;
        private readonly VisitorViewFactory _visitorViewFactory;
        private readonly TavernMood _tavernMood;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorSpawnService
        (
            IPrefabFactory prefabFactory,
            ObjectPool<VisitorView> objectPool,
            VisitorViewFactory visitorViewFactory,
            ITavernProvider tavernProvider
        )
        {
            //TODO Нет проверок на нулл
            _gamePlay = tavernProvider.GamePlay;
            _tavernMood = tavernProvider.TavernMood;
            _visitorCounter = new VisitorCounter();
            
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
            _visitorViewFactory = visitorViewFactory ?? throw new ArgumentNullException(nameof(visitorViewFactory));
        }

        private bool CanSpawn => _visitorCounter.ActiveVisitorsCount < _gamePlay.MaximumVisitorsCapacity;

        //TODO не забыть запустить этот метод
        public async void SpawnVisitorAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            while (true)
            {
                if (CanSpawn)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(Constant.Visitors.SpawnDelay),
                        cancellationToken: _cancellationTokenSource.Token);

                    Spawn();
                }

                await UniTask.Yield(_cancellationTokenSource.Token);
            }
        }

        public void Cancel() =>
            _cancellationTokenSource.Cancel();

        private void Spawn()
        {
            Create();

            _visitorCounter.AddActiveVisitorsCount();
        }

        private IVisitorView Create()
        {
            Visitor visitor = new Visitor();

            return CreateFromPool(visitor, _tavernMood, _visitorCounter) ??
                   _visitorViewFactory.Create(visitor, _tavernMood, _visitorCounter);
        }

        private IVisitorView CreateFromPool(Visitor visitor, TavernMood tavernMood, VisitorCounter visitorCounter)
        {
            VisitorView visitorView = _objectPool.Get<VisitorView>();

            if (visitorView == null)
                return null;

            return _visitorViewFactory.Create(visitor, tavernMood, visitorCounter, visitorView);
        }
    }
}