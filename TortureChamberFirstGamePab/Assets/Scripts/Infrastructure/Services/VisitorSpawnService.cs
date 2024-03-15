using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Domain.Constants;
using Scripts.Domain.GamePlays;
using Scripts.Domain.Taverns;
using Scripts.Domain.Visitors;
using Scripts.Infrastructure.Factories.Views.Visitors;
using Scripts.Infrastructure.Services.ObjectPools;
using Scripts.InfrastructureInterfaces.Services;
using Scripts.InfrastructureInterfaces.Services.Providers.Taverns;
using Scripts.Presentation.Views.GamePoints.VisitorsPoints;
using Scripts.Presentation.Views.Visitors;
using Scripts.PresentationInterfaces.Views.Visitors;
using Scripts.Utils.Repositories.CollectionRepository;

namespace Scripts.Infrastructure.Services
{
    public class VisitorSpawnService : IVisitorSpawnService
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly ObjectPool<VisitorView> _objectPool;
        private readonly ITavernProvider _tavernProvider;
        private readonly VisitorCounter _visitorCounter;
        private readonly VisitorViewFactory _visitorViewFactory;

        private CancellationTokenSource _cancellationTokenSource;

        private TavernMood _tavernMood;
        private TimeSpan _timeSpan;
        private VisitorQuantity _visitorQuantity;

        public VisitorSpawnService(
            ObjectPool<VisitorView> objectPool,
            VisitorViewFactory visitorViewFactory,
            ITavernProvider tavernProvider,
            CollectionRepository collectionRepository)
        {
            _visitorCounter = new VisitorCounter();

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
            _timeSpan = TimeSpan.FromSeconds(VisitorConstant.SpawnDelay);

            SpawnVisitorAsync(_cancellationTokenSource.Token);
        }

        public void Exit() =>
            _cancellationTokenSource.Cancel();

        private async void SpawnVisitorAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (cancellationToken.IsCancellationRequested == false)
                {
                    if (CanSpawn())
                    {
                        await UniTask.Delay(_timeSpan, cancellationToken: cancellationToken);

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
            VisitorView visitorView = _objectPool.Get<VisitorView>();

            if (visitorView == null)
                return null;

            return _visitorViewFactory.Create(visitor, tavernMood, visitorCounter, visitorView);
        }
    }
}