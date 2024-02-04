using System.Collections.Generic;
using Agava.YandexGames;
using Sources.Domain.YandexSDC;
using UnityEngine;

namespace Sources.Presentation.Views.YandexSDC.Agava
{
    public class YandexLeaderboard : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const string AnonymousName = "Anonymous";

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();

        [SerializeField] private LeaderboardView _leaderboardView;

        public void SetPlayerScore(int score)
        {
            if(PlayerAccount.IsAuthorized == false)
                return;
            
            Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
            {
                if(result.score < score)
                    Leaderboard.SetScore(LeaderboardName, score);
            });
        }

        public void Fill()
        {
            _leaderboardPlayers.Clear();
            
            if(PlayerAccount.IsAuthorized)
                return;
            
            Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = AnonymousName;
                    
                    _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
                }
                
                _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
            });
        }
    }
}