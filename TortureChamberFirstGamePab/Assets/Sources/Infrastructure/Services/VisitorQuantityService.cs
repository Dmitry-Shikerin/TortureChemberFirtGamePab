using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.GamePlays;
using Sources.Domain.Points;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class VisitorQuantityService : IQuantityService
    {
        private readonly ITavernProvider _tavernProvider;
        private readonly IPauseService _pauseService;
        private readonly int _maximumSetPointsCapacity;
        private VisitorQuantity _visitorQuantity;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorQuantityService
        (
            ITavernProvider tavernProvider,
            VisitorPoints visitorPoints,
            IPauseService pauseService
        )
        {
            _tavernProvider = tavernProvider ?? throw new ArgumentNullException(nameof(tavernProvider));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
            _maximumSetPointsCapacity = visitorPoints.GetComponentsInChildren<SeatPointView>().Length;

            if (_maximumSetPointsCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(_maximumSetPointsCapacity));
        }

        private VisitorQuantity VisitorQuantity => _visitorQuantity ??= _tavernProvider.VisitorQuantity;
        
        public async void Enter(object payload = null)
        {
            await IncreaseDifficulty();
        }
        
        private async UniTask IncreaseDifficulty()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            int visitorsCount = 0;

            //TODO что сделать с этим трай кетчем
            try
            {
                while (visitorsCount <= _maximumSetPointsCapacity)
                {
                    await UniTask.Delay(TimeSpan.FromMinutes(Constant.GamePlay.SpawnDelay),
                        cancellationToken: _cancellationTokenSource.Token);
                    Debug.Log("увеличино максимальное количество посетителей");
                    visitorsCount++;
                    VisitorQuantity.AddMaximumVisitorsQuantity();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void Exit()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}