using System;
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
        
        //TODO сделать сохранение привыходе из игры, и сохранять игру по таймеру
        //TODo разделить туториал на несколько окошек и переключать их
        //TODO сделать проверку в туториале есть ли у человека какойто скор чтобы не показывать его постоянно
        //TODO разделить этот сервис
        public void Fill()
        {
            if (PlayerAccount.IsAuthorized)
                return;
            
            Leaderboard.GetEntries(Constant.LeaderboardNames.LeaderboardName,
                (result) =>
                {
                    for (int i = 0; i < _leaderboardElementViewContainer.LeaderboardElementViews.Count; i++)
                    {
                        _leaderboardElementViewFactory.Create(new LeaderboardPlayer(result.entries[i]),
                            _leaderboardElementViewContainer.LeaderboardElementViews[i]);
                    }
                });
        }
    }
}