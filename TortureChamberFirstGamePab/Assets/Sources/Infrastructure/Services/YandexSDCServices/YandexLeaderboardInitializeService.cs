using System;
using Agava.WebUtility;
using Agava.YandexGames;
using Sources.Domain.Constants;
using Sources.Domain.YandexSDC;
using Sources.Infrastructure.Factories.Views.YandexSDC;
using Sources.Presentation.Views.YandexSDC.MyVariant;

namespace Sources.Infrastructure.Services.YandexSDCServices
{
    public class YandexLeaderboardInitializeService : ILeaderboardInitializeService
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

        public void Fill()
        {
            if (WebApplication.IsRunningOnWebGL == false)
                return;

            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetEntries(Constant.LeaderboardNames.LeaderboardName,
                (result) =>
                {
                    int count = result.entries.Length < _leaderboardElementViewContainer.LeaderboardElementViews.Count
                        ? result.entries.Length
                        : _leaderboardElementViewContainer.LeaderboardElementViews.Count;

                    for (int i = 0; i < count; i++)
                    {
                        int rank = result.entries[i].rank;
                        int score = result.entries[i].score;
                        string name = result.entries[i].player.publicName;

                        if (string.IsNullOrEmpty(name))
                        {
                            name = YandexGamesSdk.Environment.i18n.lang switch
                            {
                                Constant.Localization.English => Constant.Anonymous.English,
                                Constant.Localization.Turkish => Constant.Anonymous.Turkish,
                                Constant.Localization.Russian => Constant.Anonymous.Russian,
                                _ => Constant.Anonymous.English
                            };
                        }

                        _leaderboardElementViewFactory.Create(new LeaderboardPlayer(rank, name, score),
                            _leaderboardElementViewContainer.LeaderboardElementViews[i]);
                    }
                });
        }
    }
}