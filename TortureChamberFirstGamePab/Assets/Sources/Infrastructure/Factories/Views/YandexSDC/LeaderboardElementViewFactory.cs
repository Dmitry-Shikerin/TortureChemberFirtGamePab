using System;
using Sources.Controllers.YandexSDC;
using Sources.Domain.YandexSDC;
using Sources.Infrastructure.Factories.Controllers.YandexSDC;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Views.YandexSDC.MyVariant;
using Sources.PresentationInterfaces.Views.YandexSDC;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Views.YandexSDC
{
    public class LeaderboardElementViewFactory
    {
        private readonly LeaderboardElementPresenterFactory _leaderboardElementPresenterFactory;
        private readonly IPrefabFactory _prefabFactory;

        public LeaderboardElementViewFactory
        (
            LeaderboardElementPresenterFactory leaderboardElementPresenterFactory,
            IPrefabFactory prefabFactory
        )
        {
            _leaderboardElementPresenterFactory =
                leaderboardElementPresenterFactory ??
                throw new ArgumentNullException(nameof(leaderboardElementPresenterFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
        }

        public ILeaderboardElementView Create(LeaderboardPlayer leaderboardPlayer)
        {
            //TODO указать папку префаба
            LeaderboardElementView leaderboardElementView =
                _prefabFactory.Create<LeaderboardElementView>();

            LeaderboardElementPresenter leaderboardElementPresenter =
                _leaderboardElementPresenterFactory.Create(leaderboardPlayer, leaderboardElementView);
            
            leaderboardElementView.Construct(leaderboardElementPresenter);

            return leaderboardElementView;
        }
        
        public ILeaderboardElementView Create(LeaderboardPlayer leaderboardPlayer, 
            LeaderboardElementView leaderboardElementView)
        {
            LeaderboardElementPresenter leaderboardElementPresenter =
                _leaderboardElementPresenterFactory.Create(leaderboardPlayer, leaderboardElementView);
            
            leaderboardElementView.Construct(leaderboardElementPresenter);

            return leaderboardElementView;
        }
    }
}