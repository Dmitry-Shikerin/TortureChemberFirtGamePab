using System;
using Agava.YandexGames;
using Sources.Domain.Constants;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class YandexLeaderboardScoreSetter : ILeaderboardScoreSetter
    {
        private readonly IWebGlService _webGlService;

        public YandexLeaderboardScoreSetter(IWebGlService webGlService)
        {
            _webGlService = webGlService ?? throw new ArgumentNullException(nameof(webGlService));
        }

        public void SetPlayerScore(int score)
        {
            if(_webGlService.IsWebGl == false)
                return;
            
            if (PlayerAccount.IsAuthorized == false)
                return;

            //TODO это запись чисто игрока
            Leaderboard.GetPlayerEntry(Constant.LeaderboardNames.LeaderboardName,
                (result) =>
                {
                    if (result.score < score)
                        Leaderboard.SetScore(Constant.LeaderboardNames.LeaderboardName, score);
                });
        }
    }
}