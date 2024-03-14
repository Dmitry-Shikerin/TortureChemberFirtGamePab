using System;
using System.Linq;
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
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Views.GamePoints.VisitorsPoints;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Repositoryes.CollectionRepository;

namespace Sources.Infrastructure.Services
{
    public class VisitorSpawnService : IVisitorSpawnService
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly ObjectPool<VisitorView> _objectPool;
        private readonly IPauseService _pauseService;
        private readonly IPrefabFactory _prefabFactory;
        private readonly ITavernProvider _tavernProvider;
        private readonly VisitorCounter _visitorCounter;
        private readonly VisitorViewFactory _visitorViewFactory;

        private CancellationTokenSource _cancellationTokenSource;

        private TavernMood _tavernMood;
        private TimeSpan _timeSpan;
        private VisitorQuantity _visitorQuantity;

        public VisitorSpawnService(
            IPauseService pauseService,
            IPrefabFactory prefabFactory,
            ObjectPool<VisitorView> objectPool,
            VisitorViewFactory visitorViewFactory,
            ITavernProvider tavernProvider,
            CollectionRepository collectionRepository)
        {
            _visitorCounter = new VisitorCounter();

            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
            _visitorViewFactory = visitorViewFactory ?? throw new ArgumentNullException(nameof(visitorViewFactory));
            _tavernProvider = tavernProvider ?? throw new ArgumentNullException(nameof(tavernProvider));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
        }

        private VisitorQuantity VisitorQuantity => _visitorQuantity ??= _tavernProvider.VisitorQuantity;
        private TavernMood TavernMood => _tavernMood ??= _tavernProvider.TavernMood;

        public void Enter(object payload = null)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _timeSpan = TimeSpan.FromSeconds(Constant.Visitors.SpawnDelay);

            SpawnVisitorAsync(_cancellationTokenSource.Token);
        }

        public void Exit()
        {
            _cancellationTokenSource.Cancel();
        }

        private async void SpawnVisitorAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    if (CanSpawn())
                    {
                        await UniTask.Delay(_timeSpan,
                            cancellationToken: cancellationToken);

                        Spawn();
                    }

                    await UniTask.Yield(cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private bool CanSpawn()
        {
            var freeSeatPoints = _collectionRepository
                .Get<SeatPointView>()
                .Count(seatPoint => seatPoint.IsOccupied == false);

            return _visitorCounter.ActiveVisitorsCount < VisitorQuantity.MaximumVisitorsQuantity &&
                   _visitorCounter.ActiveVisitorsCount < freeSeatPoints;
        }

        private void Spawn()
        {
            Create();

            _visitorCounter.AddActiveVisitorsCount();
        }

        private IVisitorView Create()
        {
            var visitor = new Visitor();

            return CreateFromPool(visitor, TavernMood, _visitorCounter) ??
                   _visitorViewFactory.Create(visitor, TavernMood, _visitorCounter);
        }

        private IVisitorView CreateFromPool(Visitor visitor, TavernMood tavernMood, VisitorCounter visitorCounter)
        {
            var visitorView = _objectPool.Get<VisitorView>();

            if (visitorView == null)
                return null;

            return _visitorViewFactory.Create(visitor, tavernMood, visitorCounter, visitorView);
        }
    }
}