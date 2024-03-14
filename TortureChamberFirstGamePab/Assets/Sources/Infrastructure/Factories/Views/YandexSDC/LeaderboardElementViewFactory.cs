using System;
using Sources.Domain.YandexSDC;
using Sources.Infrastructure.Factories.Controllers.YandexSDC;
using Sources.Presentation.Views.YandexSDC.MyVariant;
using Sources.PresentationInterfaces.Views.YandexSDC;

namespace Sources.Infrastructure.Factories.Views.YandexSDC
{
    public class LeaderboardElementViewFactory
    {
        private readonly LeaderboardElementPresenterFactory _leaderboardElementPresenterFactory;

        public LeaderboardElementViewFactory(LeaderboardElementPresenterFactory leaderboardElementPresenterFactory)
        {
            _leaderboardElementPresenterFactory =
                leaderboardElementPresenterFactory ??
                throw new ArgumentNullException(nameof(leaderboardElementPresenterFactory));
        }

        public ILeaderboardElementView Create(
            LeaderboardPlayer leaderboardPlayer,
            LeaderboardElementView leaderboardElementView)
        {
            if (leaderboardPlayer == null)
                throw new ArgumentNullException(nameof(leaderboardPlayer));

            if (leaderboardElementView == null)
                throw new ArgumentNullException(nameof(leaderboardElementView));

            var leaderboardElementPresenter =
                _leaderboardElementPresenterFactory.Create(leaderboardPlayer, leaderboardElementView);

            leaderboardElementView.Construct(leaderboardElementPresenter);

            return leaderboardElementView;
        }
    }
}