using System;
using System.Linq;
using Agava.WebUtility;
using Agava.YandexGames;
using Sources.Domain.Constants;
using Sources.Domain.YandexSDC;
using Sources.Infrastructure.Factories.Views.YandexSDC;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
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

            if (PlayerAccount.IsAuthorized)
                return;

            Leaderboard.GetEntries(Constant.LeaderboardNames.LeaderboardName,
                (result) =>
                {
                    int count = result.entries.Length < _leaderboardElementViewContainer.LeaderboardElementViews.Count
                        ? result.entries.Length
                        : _leaderboardElementViewContainer.LeaderboardElementViews.Count;

                    for (int i = 0; i < count; i++)
                    {
                        _leaderboardElementViewFactory.Create(new LeaderboardPlayer(result.entries[i]),
                            _leaderboardElementViewContainer.LeaderboardElementViews[i]);
                    }
                });
        }
    }
}