using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.GamePlays;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Services.ObjectPolls;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Services
{
    public class VisitorSpawnService
    {
        private readonly GamePlay _gamePlay;
        private readonly VisitorCounter _visitorCounter;
        private readonly PrefabFactory _prefabFactory;
        private readonly IObjectPool _objectPool;
        private readonly VisitorViewFactory _visitorViewFactory;
        private readonly TavernMood _tavernMood;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorSpawnService(GamePlay gamePlay
            , VisitorCounter visitorCounter,
            PrefabFactory prefabFactory, ObjectPool<VisitorView> objectPool,
            VisitorViewFactory visitorViewFactory,
            TavernMood tavernMood)
        {
            _gamePlay = gamePlay ?? throw new ArgumentNullException(nameof(gamePlay));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
            _visitorViewFactory = visitorViewFactory ?? throw new ArgumentNullException(nameof(visitorViewFactory));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
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
                    await UniTask.Delay(TimeSpan.FromSeconds(Constant.VisitorSpawnDelay), 
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
        
        public IVisitorView Create()
        {
            Visitor visitor = new Visitor();
            
            return CreateFromPool(visitor, _tavernMood, _visitorCounter) ?? 
                   _visitorViewFactory.Create(visitor, _tavernMood, _visitorCounter);
        }

        public IVisitorView CreateFromPool(Visitor visitor, TavernMood tavernMood, VisitorCounter visitorCounter)
        {
            VisitorView visitorView = _objectPool.Get<VisitorView>();

            if (visitorView == null)
                return null;

            return _visitorViewFactory.Create(visitor, tavernMood, visitorCounter, visitorView);
        }
    }
}