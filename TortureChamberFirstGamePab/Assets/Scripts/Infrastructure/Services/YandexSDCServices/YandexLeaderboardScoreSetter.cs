using Agava.WebUtility;
using Agava.YandexGames;
using Scripts.Domain.Constants;
using Scripts.InfrastructureInterfaces.Services.SDCServices;

namespace Scripts.Infrastructure.Services.YandexSDCServices
{
    public class YandexLeaderboardScoreSetter : ILeaderboardScoreSetter
    {
        public void SetPlayerScore(int score)
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetPlayerEntry(LeaderboardName.Leaderboard, result =>
                {
                    if (result.score < score)
                        Leaderboard.SetScore(LeaderboardName.Leaderboard, score);
                });
        }
    }
}