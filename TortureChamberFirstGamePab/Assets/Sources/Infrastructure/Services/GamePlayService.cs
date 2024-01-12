using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.Domain.GamePlays;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class GamePlayService
    {
        private const int Delay = 2;
        
        private readonly GamePlay _gamePlay;

        private readonly int _maximumSetPointsCapacity;

        private CancellationTokenSource _cancellationTokenSource;

        public GamePlayService(GamePlay gamePlay, int maximumSetPointsCapacity)
        {
            if (maximumSetPointsCapacity <= 0) 
                throw new ArgumentOutOfRangeException(nameof(maximumSetPointsCapacity));
            
            _gamePlay = gamePlay ?? throw new ArgumentNullException(nameof(gamePlay));
            _maximumSetPointsCapacity = maximumSetPointsCapacity;
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
                await UniTask.Delay(TimeSpan.FromMinutes(Delay), cancellationToken: _cancellationTokenSource.Token);
                Debug.Log("увеличино максимальное количество посетителей");
                visitorsCount++;
                _gamePlay.AddMaximumVisitorsCapacity();
            }
        }

        public void Exit() => 
            _cancellationTokenSource.Cancel();
    }
}