using System.Collections.Generic;
using Sources.Domain.YandexSDC;
using UnityEngine;

namespace Sources.Presentation.Views.YandexSDC
{
    public class LeaderboardView : View
    {
        [SerializeField] private Transform _container;
        [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

        private List<LeaderboardElement> _spawnElements = new List<LeaderboardElement>();

        public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardPlayers)
        {
            ClearLeaderboard();

            foreach (LeaderboardPlayer player in leaderboardPlayers)
            {
                LeaderboardElement leaderboardElementInstance = 
                    Instantiate(_leaderboardElementPrefab, _container);
                leaderboardElementInstance.Initialize(player.Name, player.Rank, player.Score);
                
                _spawnElements.Add(leaderboardElementInstance);
            }
        }

        private void ClearLeaderboard()
        {
            foreach (LeaderboardElement element in _spawnElements)
            {
                Destroy(element);
            }

            _spawnElements = new List<LeaderboardElement>();
        }
    }
}