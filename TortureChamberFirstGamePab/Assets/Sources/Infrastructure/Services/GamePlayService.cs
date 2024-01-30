using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.GamePlays;
using Sources.Domain.Points;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class GamePlayService
    {
        private readonly GamePlay _gamePlay;
        private readonly int _maximumSetPointsCapacity;

        private CancellationTokenSource _cancellationTokenSource;

        public GamePlayService
        (
            ITavernProvider tavernProvider,
            VisitorPoints visitorPoints
        )
        {
            _maximumSetPointsCapacity = visitorPoints.GetComponentsInChildren<SeatPointView>().Length;

            if (_maximumSetPointsCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(_maximumSetPointsCapacity));

            //TODO нету проверки на нулл что бы не поламалось
            _gamePlay = tavernProvider.GamePlay;
        }

        public async void Start()
        {
            await IncreaseDifficulty();
        }

        private async UniTask IncreaseDifficulty()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            int visitorsCount = 0;

            while (visitorsCount <= _maximumSetPointsCapacity)
            {
                await UniTask.Delay(TimeSpan.FromMinutes(Constant.GamePlay.SpawnDelay),
                    cancellationToken: _cancellationTokenSource.Token);
                Debug.Log("увеличино максимальное количество посетителей");
                visitorsCount++;
                _gamePlay.AddMaximumVisitorsCapacity();
            }
        }

        public void Exit() =>
            _cancellationTokenSource.Cancel();
    }
}