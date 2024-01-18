using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.GamePlays;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Builders;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Services.ObjectPolls;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Visitors;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Services
{
    public class VisitorSpawnService
    {
        private const string VisitorPrefabPath = "Prefabs/Visitor";

        private readonly GamePlay _gamePlay;
        private readonly VisitorBuilder _visitorBuilder;
        private readonly VisitorCounter _visitorCounter;
        private readonly PrefabFactory _prefabFactory;
        private readonly IObjectPool _objectPool;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorSpawnService(GamePlay gamePlay,
            VisitorBuilder visitorBuilder, VisitorCounter visitorCounter,
            PrefabFactory prefabFactory)
        {
            _gamePlay = gamePlay ?? throw new ArgumentNullException(nameof(gamePlay));
            _visitorBuilder = visitorBuilder ?? throw new ArgumentNullException(nameof(visitorBuilder));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));

            _objectPool = new ObjectPool<VisitorView>();
        }

        private bool CanSpawn => _visitorCounter.ActiveVisitorsCount < _gamePlay.MaximumVisitorsCapacity;

        public async void SpawnVisitorAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            while (true)
            {
                if (CanSpawn)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: _cancellationTokenSource.Token);
                    Spawn();
                }

                await UniTask.Yield(_cancellationTokenSource.Token);
            }
        }

        public void Cancel() =>
            _cancellationTokenSource.Cancel();

        private void Spawn()
        {
            VisitorView visitorView = _objectPool.Get<VisitorView>() ??
                                      _prefabFactory.Create<VisitorView>(VisitorPrefabPath)
                                          .AddComponent<PoolableObject>()
                                          .SetPool(_objectPool)
                                          .GetComponent<VisitorView>();

            _visitorBuilder.Create(visitorView);

            _visitorCounter.AddActiveVisitorsCount();
        }
    }
}