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
        private TimeSpan _timeSpan;

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
            _cancellationTokenSource = new CancellationTokenSource();
            _timeSpan = TimeSpan.FromMinutes(Constant.GamePlay.SpawnDelay);
            
            await IncreaseDifficulty(_cancellationTokenSource.Token);
        }
        
        private async UniTask IncreaseDifficulty(CancellationToken cancellationToken)
        {
            int visitorsCount = 0;

            try
            {
                while (visitorsCount <= _maximumSetPointsCapacity)
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

        public void Exit() => 
            _cancellationTokenSource.Cancel();
    }
}