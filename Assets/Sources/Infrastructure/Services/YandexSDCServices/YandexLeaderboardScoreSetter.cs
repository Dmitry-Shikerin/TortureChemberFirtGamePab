using Agava.WebUtility;
using Agava.YandexGames;
using Domain.Constants;
using InfrastructureInterfaces.Services.SDCServices;

namespace Infrastructure.Services.YandexSDCServices
{
    public class YandexLeaderboardScoreSetter : ILeaderboardScoreSetter
    {
        public void SetPlayerScore(int score)
        {
            // Yandex integration is disabled for the itch.io release.
            // if (WebApplication.IsRunningOnWebGL == false)
            //     return;
            //
            // if (PlayerAccount.IsAuthorized == false)
            //     return;
            //
            // Leaderboard.GetPlayerEntry(LeaderboardName.Leaderboard, result =>
            //     {
            //         if (result.score < score)
            //             Leaderboard.SetScore(LeaderboardName.Leaderboard, score);
            //     });
            //
        }
    }
}