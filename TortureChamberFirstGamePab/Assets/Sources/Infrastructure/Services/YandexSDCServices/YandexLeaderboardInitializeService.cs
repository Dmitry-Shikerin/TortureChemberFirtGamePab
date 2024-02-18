using System;
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
        private readonly IWebGlService _webGlService;
        private readonly LeaderboardElementViewContainer _leaderboardElementViewContainer;

        public YandexLeaderboardInitializeService
        (
            LeaderboardElementViewContainer leaderboardElementViewContainer,
            LeaderboardElementViewFactory leaderboardElementViewFactory,
            IWebGlService webGlService
        )
        {
            _leaderboardElementViewFactory = leaderboardElementViewFactory ??
                                             throw new ArgumentNullException(nameof(leaderboardElementViewFactory));
            _webGlService = webGlService ?? throw new ArgumentNullException(nameof(webGlService));
            _leaderboardElementViewContainer = leaderboardElementViewContainer
                ? leaderboardElementViewContainer
                : throw new ArgumentNullException(nameof(leaderboardElementViewContainer));
        }
        
        //TODo разделить туториал на несколько окошек и переключать их
        //TODO сделать проверку в туториале есть ли у человека какойто скор чтобы не показывать его постоянно
        public void Fill()
        {
            if(_webGlService.IsWebGl == false)
                return;
            
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