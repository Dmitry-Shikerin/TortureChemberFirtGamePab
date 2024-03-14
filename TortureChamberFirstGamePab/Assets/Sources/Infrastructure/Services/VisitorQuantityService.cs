using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.GamePlays;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Services;
using Sources.Presentation.Views.GamePoints.VisitorsPoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;

namespace Sources.Infrastructure.Services
{
    public class VisitorQuantityService : IQuantityService
    {
        private readonly int _maximumSetPointsCapacity;
        private readonly ITavernProvider _tavernProvider;

        private CancellationTokenSource _cancellationTokenSource;
        private TimeSpan _timeSpan;
        private VisitorQuantity _visitorQuantity;

        public VisitorQuantityService(
            ITavernProvider tavernProvider,
            VisitorPoints visitorPoints)
        {
            _tavernProvider = tavernProvider ?? throw new ArgumentNullException(nameof(tavernProvider));
            _maximumSetPointsCapacity = visitorPoints.GetComponentsInChildren<SeatPointView>().Length;

            if (_maximumSetPointsCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(_maximumSetPointsCapacity));
        }

        private VisitorQuantity VisitorQuantity => _visitorQuantity ??= _tavernProvider.VisitorQuantity;

        public async void Enter(object payload = null)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _timeSpan = TimeSpan.FromSeconds(Constant.GamePlay.SpawnDelay);

            await IncreaseDifficulty(_cancellationTokenSource.Token);
        }

        public void Exit()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTask IncreaseDifficulty(CancellationToken cancellationToken)
        {
            var visitorsCount = 0;

            try
            {
                while (visitorsCount < _maximumSetPointsCapacity)
                {
                    await UniTask.Delay(_timeSpan,
                        cancellationToken: cancellationToken);

                    visitorsCount++;
                    VisitorQuantity.AddMaximumVisitorsQuantity();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}