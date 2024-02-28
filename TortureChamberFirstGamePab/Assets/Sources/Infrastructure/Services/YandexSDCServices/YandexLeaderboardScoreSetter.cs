using Agava.WebUtility;
using Agava.YandexGames;
using Sources.Domain.Constants;
using UnityEngine;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class YandexLeaderboardScoreSetter : ILeaderboardScoreSetter
    {
        public void SetPlayerScore(int score)
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (PlayerAccount.IsAuthorized == false)
                return;
            
            Debug.Log($"{nameof(YandexLeaderboardScoreSetter)} new score {score}");

            Leaderboard.GetPlayerEntry(Constant.LeaderboardNames.LeaderboardName,
                (result) =>
                {
                    if (result.score < score)
                        Leaderboard.SetScore(Constant.LeaderboardNames.LeaderboardName, score);
                });
        }
    }
}