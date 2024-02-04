using System;
using Agava.YandexGames;
using Sources.Domain.Constants;
using Sources.Domain.YandexSDC;
using Sources.Infrastructure.Factories.Views.YandexSDC;
using Sources.Presentation.Views.YandexSDC.MyVariant;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class YandexLeaderboardInitializeService
    {
        private readonly LeaderboardElementViewFactory _leaderboardElementViewFactory;
        private readonly LeaderboardElementViewContainer _leaderboardElementViewContainer;

        public YandexLeaderboardInitializeService
        (
            LeaderboardElementViewContainer leaderboardElementViewContainer,
            LeaderboardElementViewFactory leaderboardElementViewFactory
        )
        {
            _leaderboardElementViewFactory = leaderboardElementViewFactory ??
                                             throw new ArgumentNullException(nameof(leaderboardElementViewFactory));
            _leaderboardElementViewContainer = leaderboardElementViewContainer
                ? leaderboardElementViewContainer
                : throw new ArgumentNullException(nameof(leaderboardElementViewContainer));
        }

        //TODO где это вызывать?
        public void SetPlayerScore(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetPlayerEntry(Constant.LeaderboardNames.LeaderboardName,
                (result) =>
                {
                    if (result.score < score)
                        Leaderboard.SetScore(Constant.LeaderboardNames.LeaderboardName, score);
                });
        }

        public void Fill()
        {
            if (PlayerAccount.IsAuthorized)
                return;

            Leaderboard.GetEntries(Constant.LeaderboardNames.LeaderboardName,
                (result) =>
                {
                    for (int i = 0; i < _leaderboardElementViewContainer.LeaderboardElementViews.Count; i++)
                    {
                        int rank = result.entries[i].rank;
                        int score = result.entries[i].score;
                        string name = result.entries[i].player.publicName;

                        if (string.IsNullOrEmpty(name))
                            name = Constant.LeaderboardNames.AnonymousName;

                        _leaderboardElementViewFactory.Create(new LeaderboardPlayer(rank, name, score),
                            _leaderboardElementViewContainer.LeaderboardElementViews[i]);
                    }
                });
        }
    }
}