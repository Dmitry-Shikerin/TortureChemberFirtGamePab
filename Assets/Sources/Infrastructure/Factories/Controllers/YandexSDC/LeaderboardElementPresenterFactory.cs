using System;
using Controllers.YandexSDC;
using Domain.YandexSDC;
using PresentationInterfaces.Views.YandexSDC;

namespace Infrastructure.Factories.Controllers.YandexSDC
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