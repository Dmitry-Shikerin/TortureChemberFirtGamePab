using System;
using Scripts.Controllers.YandexSDC;
using Scripts.Domain.YandexSDC;
using Scripts.PresentationInterfaces.Views.YandexSDC;

namespace Scripts.Infrastructure.Factories.Controllers.YandexSDC
{
    public class LeaderboardElementPresenterFactory
    {
        public LeaderboardElementPresenter Create(
            LeaderboardPlayer leaderboardPlayer,
            ILeaderboardElementView leaderboardElementView)
        {
            if (leaderboardPlayer == null)
                throw new ArgumentNullException(nameof(leaderboardPlayer));

            if (leaderboardElementView == null)
                throw new ArgumentNullException(nameof(leaderboardElementView));

            return new LeaderboardElementPresenter(leaderboardPlayer, leaderboardElementView);
        }
    }
}