using System;
using Controllers.YandexSDC;
using Domain.YandexSDC;
using Infrastructure.Factories.Controllers.YandexSDC;
using Presentation.Views.YandexSDC.MyVariant;
using PresentationInterfaces.Views.YandexSDC;

namespace Infrastructure.Factories.Views.YandexSDC
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