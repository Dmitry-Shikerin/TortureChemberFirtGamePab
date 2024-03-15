using System;
using Agava.WebUtility;
using Agava.YandexGames;
using Scripts.Domain.Constants;
using Scripts.Domain.YandexSDC;
using Scripts.Infrastructure.Factories.Views.YandexSDC;
using Scripts.InfrastructureInterfaces.Services.SDCServices;
using Scripts.Presentation.Views.YandexSDC.MyVariant;

namespace Scripts.Infrastructure.Services.YandexSDCServices
{
    public class YandexLeaderboardInitializeService : ILeaderboardInitializeService
    {
        private readonly LeaderboardElementViewContainer _leaderboardElementViewContainer;
        private readonly LeaderboardElementViewFactory _leaderboardElementViewFactory;

        public YandexLeaderboardInitializeService(
            LeaderboardElementViewContainer leaderboardElementViewContainer,
            LeaderboardElementViewFactory leaderboardElementViewFactory)
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

            Leaderboard.GetEntries(LeaderboardName.Leaderboard, result =>
                {
                    var count = result.entries.Length < _leaderboardElementViewContainer.LeaderboardElementViews.Count
                        ? result.entries.Length
                        : _leaderboardElementViewContainer.LeaderboardElementViews.Count;

                    for (var i = 0; i < count; i++)
                    {
                        var rank = result.entries[i].rank;
                        var score = result.entries[i].score;
                        var name = result.entries[i].player.publicName;

                        if (string.IsNullOrEmpty(name))
                            name = YandexGamesSdk.Environment.i18n.lang switch
                            {
                                LocalizationConstant.English => Anonymous.English,
                                LocalizationConstant.Turkish => Anonymous.Turkish,
                                LocalizationConstant.Russian => Anonymous.Russian,
                                _ => Anonymous.English
                            };

                        _leaderboardElementViewFactory.Create(
                            new LeaderboardPlayer(rank, name, score),
                            _leaderboardElementViewContainer.LeaderboardElementViews[i]);
                    }
                });
        }
    }
}