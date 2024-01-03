using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.Domain.GamePlays;

namespace Sources.Infrastructure.Services
{
    public class GamePlayService
    {
        private readonly GamePlay _gamePlay;

        private readonly int _maximumSetPointsCapacity;

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
            int visitorsCount = 0;
            
            while (visitorsCount <= _maximumSetPointsCapacity)
            {
                await Task.Delay(TimeSpan.FromMinutes(2));

                visitorsCount++;
                _gamePlay.AddMaximumVisitorsCapacity();
                //todo заменить магическое число
            }
        }
    }
}