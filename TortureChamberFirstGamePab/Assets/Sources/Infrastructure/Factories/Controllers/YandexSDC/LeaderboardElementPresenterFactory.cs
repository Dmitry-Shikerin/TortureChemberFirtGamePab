﻿using Sources.Controllers.YandexSDC;
using Sources.Domain.YandexSDC;
using Sources.PresentationInterfaces.Views.YandexSDC;

namespace Sources.Infrastructure.Factories.Controllers.YandexSDC
{
    public class LeaderboardElementPresenterFactory
    {
        public LeaderboardElementPresenter Create
        (
            LeaderboardPlayer leaderboardPlayer,
            ILeaderboardElementView leaderboardElementView
        )
        {
            return new LeaderboardElementPresenter
            (
                leaderboardPlayer,
                leaderboardElementView
            );
        }
    }
}