using System;
using Scripts.Controllers.YandexSDC;
using Scripts.Domain.YandexSDC;
using Scripts.Infrastructure.Factories.Controllers.YandexSDC;
using Scripts.Presentation.Views.YandexSDC.MyVariant;
using Scripts.PresentationInterfaces.Views.YandexSDC;

namespace Scripts.Infrastructure.Factories.Views.YandexSDC
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

            LeaderboardElementPresenter leaderboardElementPresenter =
                _leaderboardElementPresenterFactory.Create(leaderboardPlayer, leaderboardElementView);

            leaderboardElementView.Construct(leaderboardElementPresenter);

            return leaderboardElementView;
        }
    }
}